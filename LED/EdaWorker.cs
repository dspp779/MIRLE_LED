using GeFanuc.iFixToolkit.Adapter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LED
{
    public class EdaWorker
    {
        InstantMessage im;

        private int effect = 0;

        // signal represent if refreshment is needed or not
        private bool refreshSignal = false;
        // synchronization lock for refreshsignal
        private object signalLock = new object();

        public EdaWorker(InstantMessage im)
        {
            this.im = im;
            // start eda data refresh worker
            ThreadPool.QueueUserWorkItem(new WaitCallback(MessageRefresher));
        }

        /* it's a worker thread method responsible for
         * refreshing preview and LED display periodically.
         * */
        private void MessageRefresher(object o)
        {
            while (true)
            {
                try
                {
                    // start eda data refresh worker
                    RefreshMessageWorker(im);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(RefreshMessageWorker));
                }
                catch (Exception ex)
                {
                    // program closing, leave the refresh look
                    if (LED.settingForm.IsDisposed || LED.settingForm.Disposing)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            RefreshMessage(ex.Message,  Color.Red);
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    }
                }
                finally
                {
                    // save current effect
                    int e = effect;
                    // reset effect
                    effect = 0;
                    // spinwait : wait delay time when a animated message sent; Otherwise, wait 1 sec
                    SpinWait.SpinUntil(() => refreshSignal, e == 0 ? 1000 : LEDConfig.delayTime);
                }
            }
        }
        private void RefreshMessageWorker(InstantMessage im)
        {
            try
            {
                IMSetting ims = new IMSetting(im);
                refreshSignal = false;
                // retrieve value from ifix EDA
                float f;
                short nErr = Eda.GetOneFloat(ims.node, ims.tag, ims.field, out f);
                // set message
                if (nErr != FixError.FE_OK)
                {
                    RefreshMessage(ims.getVal("????"), Color.FromArgb(ims.color));
                }
                else
                {
                    RefreshMessage(ims.getVal(f), Color.FromArgb(ims.color));
                }
            }
            // Eda.dll not found if ifix haven't intalled
            catch (DllNotFoundException)
            {
                RefreshMessage("ifix連接失敗", Color.Red);
                LED.settingForm.RefreshStatus("請確認是否安裝ifix");
            }
        }

        // asynchronous refresh call method for settingForm call
        public void refresh(int effect)
        {
            this.effect = effect;
            refresh();
        }
        public void refresh()
        {
            if (im != null)
            {
                try
                {
                    refreshSignal = true;
                }
                catch (FormatException ex)
                {
                    RefreshMessage("格式錯誤:" + ex.Message, Color.Red);
                }
            }
        }

        // refresh message
        private void RefreshMessage(string str, Color foreColor)
        {
            // refresh preview on SettingForm
            if (LED.settingForm.IsDisposed || LED.settingForm.Disposing)
            {
                return;
            }
            LED.settingForm.RefreshPreview(str);

            // refresh message on LED
            RefreshLED(str, foreColor);
        }
        // CP5200 refresher
        private void RefreshLED(string str, Color foreColor)
        {
            //CP5200_SendText(PreviewResult.Text);

            // create temporal image
            TextImage tempImg = new TextImage(str, LEDConfig.defaultFont, foreColor, Color.Black);

            try
            {
                LEDConnection.CP5200_SendImg(tempImg, effect);
            }
            catch (Exception ex)
            {
                //refresh preview area
                LED.settingForm.RefreshPreview(ex.Message);
            }
            finally
            {
                // delete temp image
                tempImg.release();
            }
        }

    }
}
