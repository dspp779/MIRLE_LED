// CP5200.h : Header file of CP5200 DLL
/*------------------------------------------
 [API for Playbill]
    Make playbill file

 [API for Program]
	Make program file

 [API for communication]
	Make data for communicate to LED controller
	Parse the return data from LED controller
------------------------------------------*/

#ifndef _CP5200API_H_
#define _CP5200API_H_

#ifdef CP5200_EXPORTS
	#define CP5200API extern "C" __declspec(dllexport) 
#else
	#ifdef __cplusplus
		#define CP5200API extern "C" __declspec(dllimport)
	#else
		#define CP5200API __declspec(dllimport)
	#endif
#endif

#define CALLING_CONV __stdcall

typedef void* HOBJECT;

#define ERR_OBJ_INVALID			-1
#define ERR_MAKE_DATALEN		-3
#define ERR_MAKE_BUFSIZE		-4

#define ERR_PARSE_INVALID		-1
#define ERR_PARSE_COMMAND		-2
#define ERR_PARSE_LENGTH		-3
#define ERR_PARSE_BUFSIZE		-4
#define ERR_PARSE_CHKSUM		-5

#define  COMMDATA_TYPE_RS232	0
#define  COMMDATA_TYPE_NETWORK	1

//==========================================
// API for screen information
//==========================================
CP5200API void CALLING_CONV CP5200_SetScreenSize(WORD width, WORD height); //same as CP5200_SetScreenInfo, which 'byColorGray' default 119(0x77)
CP5200API void CALLING_CONV CP5200_SetScreenInfo(WORD width, WORD height, BYTE byColorGray);
//==========================================
// API for Playbill 
//==========================================
/*------------------------------------------
 Step 1: Create a playbill object
 Step 2: Add program file to the playbill
 Step 3: Save the playbill to file
 Step 4: Destroy the playbill object
------------------------------------------*/
CP5200API HOBJECT	CALLING_CONV CP5200_Playbill_Create	(WORD width, WORD height, BYTE color);
CP5200API int		CALLING_CONV CP5200_Playbill_Destroy	(HOBJECT hObj);
CP5200API int		CALLING_CONV CP5200_Playbill_AddFile	(HOBJECT hObj, const char* pFilename);
CP5200API int		CALLING_CONV CP5200_Playbill_DelFile	(HOBJECT hObj, const char* pFilename);
CP5200API int		CALLING_CONV CP5200_Playbill_SaveToFile (HOBJECT hObj, const char* pFilename);

//==========================================
// API for Program
//==========================================
/*------------------------------------------
Step 1: Create a program object
Step 2: Add play window
Step 3: Add item(text, clock, etc.) to the program
Step 4: Save the program to file
Step 5: Destroy the program object
------------------------------------------*/
CP5200API HOBJECT	CALLING_CONV CP5200_Program_Create	(WORD width, WORD height, BYTE color);
CP5200API int		CALLING_CONV CP5200_Program_Destroy	(HOBJECT hObj);
CP5200API int		CALLING_CONV CP5200_Program_SetProperty(HOBJECT hObj, int nPropertyValue, int nPropertyID);
CP5200API int		CALLING_CONV CP5200_Program_SetBackgndImage(HOBJECT hObj, const BYTE* pImgDat, WORD wImgWidth, WORD wImgHeight, BYTE color, int nMode, int nCompress);
CP5200API int		CALLING_CONV CP5200_Program_AddPlayWindow(HOBJECT hObj, WORD x, WORD y, WORD cx, WORD cy);
CP5200API int		CALLING_CONV CP5200_Program_SetWindowProperty(HOBJECT hObj, int nWinNo, int nPropertyValue, int nPropertyID);
CP5200API int		CALLING_CONV CP5200_Program_SetItemProperty(HOBJECT hObj, int nWinNo, int nItem, int nPropertyValue, int nPropertyID);
CP5200API int		CALLING_CONV CP5200_Program_AddText(HOBJECT hObj, int nWinNo, const char* pText, int nFontSize, COLORREF crColor, int nEffect, int nSpeed, int nStay);
CP5200API int		CALLING_CONV CP5200_Program_AddTagText(HOBJECT hObj, int nWinNo, const char* pText, int nFontSize, COLORREF crColor, int nEffect, int nSpeed, int nStay);
CP5200API int		CALLING_CONV CP5200_Program_AddPicture(HOBJECT hObj, int nWinNo, const char* pPictFile, int nMode, int nEffect, int nSpeed, int nStay, int nCompress);
CP5200API int		CALLING_CONV CP5200_Program_AddImage(HOBJECT hObj, int nWinNo, const BYTE* pImgDat, WORD wImgWidth, WORD wImgHeight, BYTE color, int nMode, int nEffect, int nSpeed, int nStay, int nCompress, int nPageCount);
CP5200API int		CALLING_CONV CP5200_Program_AddLafPict(HOBJECT hObj, int nWinNo, const char* pLafFile, int nMode, int nEffect, int nSpeed, int nStay, int nCompress);
CP5200API int		CALLING_CONV CP5200_Program_AddLafVideo(HOBJECT hObj, int nWinNo, const char* pLafFile, int nMode, int nRepeat);
CP5200API int		CALLING_CONV CP5200_Program_AddAnimator(HOBJECT hObj, int nWinNo, const char* pAniFile, int nMode, int nRepeat);
CP5200API int		CALLING_CONV CP5200_Program_AddClock(HOBJECT hObj, int nWinNo, const char* pText, int nFontSize, COLORREF crColor, int nStay, WORD wAttr);
CP5200API int		CALLING_CONV CP5200_Program_AddTemperature(HOBJECT hObj, int nWinNo, const char* pText, int nFontSize, COLORREF crColor, int nStay, WORD wAttr);
CP5200API int		CALLING_CONV CP5200_Program_AddVariable(HOBJECT hObj, int nWinNo, int nFontSize, COLORREF crColor, int nStay, WORD wAttr);
CP5200API int		CALLING_CONV CP5200_Program_AddTimeCounter(HOBJECT hObj, int nWinNo, int nFontSize, COLORREF crColor, int nStay, int nOption, const int *pBaseTime, const char *pContent);
CP5200API int		CALLING_CONV CP5200_Program_SaveToFile(HOBJECT hObj, const char* pFilename);

//==========================================
//API for communication with LED controller
//==========================================
CP5200API HOBJECT	CALLING_CONV CP5200_CommData_Create(int nCommType, BYTE byCardID, DWORD dwIDCode);
CP5200API int		CALLING_CONV CP5200_CommData_Destroy(HOBJECT hObj);

CP5200API int		CALLING_CONV CP5200_MakeCreateFileData(HOBJECT hObj, BYTE *pBuffer, int nBufSize, const char* pFilename, long lFilesize, const BYTE* pTimeBuffer);
CP5200API int		CALLING_CONV CP5200_ParseCreateFileRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeWriteFileData(HOBJECT hObj, BYTE *pBuffer, int nBufSize, const BYTE *pData, WORD wDatLen, WORD *pwChksum);
CP5200API int		CALLING_CONV CP5200_ParseWriteFileRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeCloseFileData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, WORD wChksum);
CP5200API int		CALLING_CONV CP5200_ParseCloseFileRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeDeleteFileNoData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, int fno);
CP5200API int		CALLING_CONV CP5200_ParseDeleteFileNoRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeDeleteFileNameData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const char *pFilename);
CP5200API int		CALLING_CONV CP5200_ParseDeleteFileNameRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeReadTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadTimeRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pTimeBuffer, int nTimeBufSize);

CP5200API int		CALLING_CONV CP5200_MakeWriteTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pTimeBuffer);
CP5200API int		CALLING_CONV CP5200_ParseWriteTimeRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeReadBrightnessData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadBrightnessRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pBrightnessBuffer, int nBrightBufSize);

CP5200API int		CALLING_CONV CP5200_MakeWriteBrightnessData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pBrightnessBuffer);
CP5200API int		CALLING_CONV CP5200_ParseWriteBrightnessRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeWriteIOOnOffTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pOnOffBuffer);
CP5200API int		CALLING_CONV CP5200_ParseWriteIOOnOffTimeRet(HOBJECT hObj, BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeReadIOOnOffTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadIOOnOffTimeRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pOnOffBuffer, int nOnOffBufSize);

CP5200API int		CALLING_CONV CP5200_MakeWriteOnOffTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pOnOffBuffer);
CP5200API int		CALLING_CONV CP5200_ParseWriteOnOffTimeRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeReadOnOffTimeData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadOnOffTimeRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pOnOffBuffer, int nOnOffBufSize);

CP5200API int		CALLING_CONV CP5200_MakeReadVersionData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadVersionRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeFormatData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseFormatRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeRestartAppData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseRestartAppRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeRestartSysData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseRestartSysRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeGetFreeSpaceData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseGetFreeSpaceRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeGetFileInfoData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, int nNewSearch);
CP5200API int		CALLING_CONV CP5200_ParseGetFileInfoRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, int pos, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_ParseGetFirstFileInfoRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);
CP5200API int		CALLING_CONV CP5200_ParseGetNextFileInfoRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, int pos, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeBeginFileUploadData(HOBJECT hObj, BYTE *pBuffer, int nBufSize, const char* pFilename, long lFilesize, const BYTE* pTimeBuffer);
CP5200API int		CALLING_CONV CP5200_ParseBeginFileUploadRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeFileUploadData(HOBJECT hObj, BYTE *pBuffer, int nBufSize, const BYTE *pData, WORD wDatLen, WORD wSegNo, WORD wSegLen, int nWantRet);
CP5200API int		CALLING_CONV CP5200_ParseFileUploadRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeEndFileUploadData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, WORD wTotalSeg);
CP5200API int		CALLING_CONV CP5200_ParseEndFileUploadRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeGetTypeInfoData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseGetTypeInfoRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);
CP5200API int		CALLING_CONV CP5200_MakeGetTempHumiData(HOBJECT hObj, BYTE* pBuffer, int nBufSize , BYTE byFlag);
CP5200API int		CALLING_CONV CP5200_ParseGetTempHumiRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeReadConfigData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, int nFlag);
CP5200API int		CALLING_CONV CP5200_ParseReadConfigRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeWriteConfigData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pConfig, int nCfgLength);
CP5200API int		CALLING_CONV CP5200_ParseWriteConfigRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

//即时信息
CP5200API int		CALLING_CONV CP5200_MakeInstantMessageData( BYTE* pBuffer, int nBufSize, BYTE byPlayTimes , int x  , int y , int cx , int cy , BYTE byFontSizeColor , int nEffect , BYTE nSpeed , BYTE byStayTime ,const char* pText ); 
CP5200API int		CALLING_CONV CP5200_MakeSendInstantMessageData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pData, int nDataLen , BYTE byLastPacket , long lDataOffset);
CP5200API int		CALLING_CONV CP5200_ParseSendInstantMessageRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize);

CP5200API int		CALLING_CONV CP5200_MakeReadHWSettingData(HOBJECT hObj, BYTE* pBuffer, int nBufSize);
CP5200API int		CALLING_CONV CP5200_ParseReadHWSettingRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize , int nPassword);

CP5200API int		CALLING_CONV CP5200_MakeWriteHWSettingData(HOBJECT hObj, BYTE* pBuffer, int nBufSize, const BYTE* pSetting,int nPassword);
CP5200API int		CALLING_CONV CP5200_ParseWriteHWSettingRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

//====================================================================================
// API for Multiwindow Data Package
//====================================================================================
CP5200API HOBJECT	CALLING_CONV CP5200_CmmPacker_Create(int nCommType, BYTE byCardID , DWORD dwIDCode);
CP5200API int		CALLING_CONV CP5200_CmmPacker_Destroy(HOBJECT hObj);
CP5200API int		CALLING_CONV CP5200_CmmPacker_Count(HOBJECT hObj);  
CP5200API int       CALLING_CONV CP5200_CmmPacker_Data(HOBJECT hObj , BYTE *pBuffer, int nBufSize, int nPackIndex ); 

CP5200API int		CALLING_CONV CP5200_MakeSplitScreenData(HOBJECT hObj, int nScrWidth, int nScrHeight, int nWndCnt, const int *pWndRects);
CP5200API int		CALLING_CONV CP5200_ParseSplitScreenRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSendTextData(HOBJECT hObj,  int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_ParseSendTextRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSendTagTextData(HOBJECT hObj,  int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_ParseSendTagTextRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSendPictureData(HOBJECT hObj,  int nWndNo, int nPosX, int nPosY, int nCx, int nCy, const char *pPictureFile, int nSpeed, int nEffect, int nStayTime, int nPictRef);
CP5200API int		CALLING_CONV CP5200_ParseSendPictureRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSendStaticData(HOBJECT hObj,  int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nAlignment, int x, int y, int cx, int cy);
CP5200API int		CALLING_CONV CP5200_ParseSendStaticRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSendClockData(HOBJECT hObj,  int nWinNo , int nStayTime , int nCalendar , int nFormat , int nContent , int nFont , int nRed , int nGreen , int nBlue ,  LPCSTR pTxt);
CP5200API int		CALLING_CONV CP5200_ParseSendClockRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeExitSplitScreenData(HOBJECT hObj);
CP5200API int		CALLING_CONV CP5200_ParseExitSplitScreenRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSaveClearWndData(HOBJECT hObj, int nSavaOrClear);
CP5200API int		CALLING_CONV CP5200_ParseSaveClearWndRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakePlaySelectedPrgData(HOBJECT hObj, const WORD *pSelected, int nSelCnt, int nOption);
CP5200API int		CALLING_CONV CP5200_ParsePlaySelectedPrgRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSetUserVarData(HOBJECT hObj, int nOption, int nVarNum , int bAstride ,  int* nVarLen , BYTE* byNoData  );
CP5200API int		CALLING_CONV CP5200_ParseSetUserVarRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSelectedAndUserVarData(HOBJECT hObj, int nOption , int nVarNum , int bAstride ,  int* nVarLen , BYTE* byNoData, int nSelPrg);
CP5200API int		CALLING_CONV CP5200_ParseSelectedAndUserVarRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSetGlobalZoneData(HOBJECT hObj,  BYTE byConfig , BYTE bySynchro , BYTE byZoneNum  ,  BYTE* pZoneMsg);
CP5200API int		CALLING_CONV CP5200_ParseSetGlobalZoneRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int       CALLING_CONV CP5200_MakePushUserVarData( HOBJECT hObj, BYTE byOption , BYTE byVarZoonNum , BYTE byVarDataLen  ,  BYTE* pVarNoAndData );
CP5200API int		CALLING_CONV CP5200_ParsePushUserVarRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int       CALLING_CONV CP5200_MakeTimerCtrlData( HOBJECT hObj, BYTE byTimerNo , BYTE byCmd  , BYTE byProp , DWORD dwValue );
CP5200API int		CALLING_CONV CP5200_ParseTimerCtrlRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CP5200_MakeSetZoneAndVariableData(HOBJECT hObj, const BYTE* pZoneData, int nZoneLen, const BYTE* pVariableData, int nVarLen, WORD wCtrl, WORD wReserved);
CP5200API int		CALLING_CONV CP5200_ParseSetZoneAndVariableRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

//====================================================================================
// API for box function
//====================================================================================
CP5200API int		CALLING_CONV CPowerBox_MakeSetProgramTemplateData(HOBJECT hObj, BYTE byColor ,USHORT nWidth , USHORT nHeight , BYTE nWndNum , BYTE* pDefParam , BYTE* pWndParam);
CP5200API int		CALLING_CONV CPowerBox_ParseSetProgramTemplateRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeInOutProgramTemplateData(HOBJECT hObj,BYTE byInOrOut );
CP5200API int		CALLING_CONV CPowerBox_ParseInOutProgramTemplateRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeQueryProgramTemplateData(HOBJECT hObj );
CP5200API int		CALLING_CONV CPowerBox_ParseQueryProgramTemplateRet(HOBJECT hObj, const BYTE* pBuffer, int nLength ,BYTE* pInfoBuffer, int nInfoBufSize );

CP5200API int		CALLING_CONV CPowerBox_MakeDeleteProgramData(HOBJECT hObj,BYTE byConfig , BYTE byProNum , BYTE* pDelPro );
CP5200API int		CALLING_CONV CPowerBox_ParseDeleteProgramRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int       CALLING_CONV CPowerBox_MakeSendTextData(HOBJECT hObj, DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byProp , BYTE* pShowFormat , char* pText);
CP5200API int		CALLING_CONV CPowerBox_ParseSendTextRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeSendPictureData(HOBJECT hObj,DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byPicType , BYTE* pShowFormat , BYTE* pPicData , long lPicDataLen);
CP5200API int		CALLING_CONV CPowerBox_ParseSendPictureRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeSetAloneProgramData(HOBJECT hObj, DWORD dwAppendCode , BYTE byProgramNo , BYTE byWindowCnt ,BYTE* pWndParam, BYTE* pWndData);
CP5200API int		CALLING_CONV CPowerBox_ParseSetAloneProgramRet(HOBJECT hObj, const BYTE* pBuffer, int nLength );

CP5200API int		CALLING_CONV CPowerBox_MakeSetProgramPropertyData(HOBJECT hObj, BYTE byOption , BYTE byProgramCnt , BYTE* pPrograms , BYTE byPropertyID1 , BYTE byPropertyID2 , BYTE byProgramLevel , USHORT nLoopCnt , USHORT nTime , BYTE* pDuetime , BYTE* pTimeInterval);
CP5200API int		CALLING_CONV CPowerBox_ParseSetProgramPropertyRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeSetScheduleData(HOBJECT hObj, DWORD dwAppendCode, BYTE byScheduleNo, const BYTE* pProperty, const BYTE* pBoxes, BYTE byBoxCnt);
CP5200API int		CALLING_CONV CPowerBox_ParseSetScheduleRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeDeleteScheduleData(HOBJECT hObj, DWORD dwAppendCode, const BYTE* pSchs, BYTE bySchCnt);
CP5200API int		CALLING_CONV CPowerBox_ParseDeleteScheduleRet(HOBJECT hObj, const BYTE* pBuffer, int nLength);

CP5200API int		CALLING_CONV CPowerBox_MakeGetScheduleData(HOBJECT hObj, DWORD dwAppendCode, BYTE byType, BYTE byScheduleNo);
CP5200API int		CALLING_CONV CPowerBox_ParseGetScheduleRet(HOBJECT hObj, const BYTE* pBuffer, int nLength, BYTE* pInfoBuffer, int nInfoBufSize );

//====================================================================================
// API for RS232
//====================================================================================
CP5200API int		CALLING_CONV CP5200_RS232_Init(const char *fName, int nBaudrate);
CP5200API int		CALLING_CONV CP5200_RS232_InitEx(const char *fName, int nBaudrate, DWORD dwTimeout);
CP5200API int		CALLING_CONV CP5200_RS232_Open(void);
CP5200API int		CALLING_CONV CP5200_RS232_OpenEx(DWORD dwReadTimeout, DWORD dwWriteTimeout);
CP5200API int		CALLING_CONV CP5200_RS232_Close(void);
CP5200API int		CALLING_CONV CP5200_RS232_IsOpened(void);
CP5200API int		CALLING_CONV CP5200_RS232_Write(const void* pBuf, int nLength);
CP5200API int		CALLING_CONV CP5200_RS232_Read(void* pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_RS232_WriteEx(const void* pBuf, int nLength);
CP5200API int		CALLING_CONV CP5200_RS232_ReadEx(void* pBuf, int nBufSize);

//====================================================================================
// API for Network
//====================================================================================
CP5200API int		CALLING_CONV CP5200_Net_Init(DWORD dwIP, int nIPPort, DWORD dwIDCode, int nTimeOut);
CP5200API int		CALLING_CONV CP5200_Net_Connect(void);
CP5200API int		CALLING_CONV CP5200_Net_IsConnected(void);
CP5200API int		CALLING_CONV CP5200_Net_Disconnect(void);
CP5200API int		CALLING_CONV CP5200_Net_Write(const BYTE* pBuf, int nLength);
CP5200API int		CALLING_CONV CP5200_Net_Read(BYTE* pBuf, int nSize);

//====================================================================================
// API for simple use
//====================================================================================
//RS232
CP5200API int		CALLING_CONV CP5200_RS232_UploadFile(int nCardID, const char* pSourceFilename, const char *pTargetFilename);
CP5200API int		CALLING_CONV CP5200_RS232_TestController(int nCardID);
CP5200API int		CALLING_CONV CP5200_RS232_TestCommunication(int nCardID);
CP5200API int		CALLING_CONV CP5200_RS232_GetTime(int nCardID, BYTE *pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_RS232_SetTime(BYTE nCardID, const BYTE *pInfo);
CP5200API int		CALLING_CONV CP5200_RS232_GetTemperatureAndHumi(int nCardID,BYTE byFlag , BYTE *pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_RS232_RestartApp(BYTE nCardID);
CP5200API int		CALLING_CONV CP5200_RS232_RestartSys(BYTE nCardID);
CP5200API int		CALLING_CONV CP5200_RS232_GetTypeInfo(BYTE nCardID, BYTE *pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_RS232_SendInstantMessage( BYTE nCardID, BYTE byPlayTimes , int x  , int y , int cx , int cy , BYTE byFontSizeColor , int nEffect , BYTE nSpeed , BYTE byStayTime ,const char* pText ); 

CP5200API int		CALLING_CONV CP5200_RS232_ReadHWSetting(BYTE nCardID, BYTE *pBuf, int nBufSize , int nPassword);
CP5200API int		CALLING_CONV CP5200_RS232_WriteHWSetting(BYTE nCardID,const BYTE *pSetting,  int nPassword);

CP5200API int		CALLING_CONV CP5200_RS232_SplitScreen(int nCardID, int nScrWidth, int nScrHeight, int nWndCnt, const int *pWndRects);
CP5200API int		CALLING_CONV CP5200_RS232_SendText(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_RS232_SendTagText(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_RS232_SendPicture(int nCardID, int nWndNo, int nPosX, int nPosY, int nCx, int nCy, const char *pPictureFile, int nSpeed, int nEffect, int nStayTime, int nPictRef);
CP5200API int		CALLING_CONV CP5200_RS232_SendStatic(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nAlignment, int x, int y, int cx, int cy);
CP5200API int       CALLING_CONV CP5200_RS232_SendClock( int nCardID, int nWinNo , int nStayTime , int nCalendar , int nFormat , int nContent , int nFont , int nRed , int nGreen , int nBlue ,  LPCSTR pTxt );
CP5200API int       CALLING_CONV CP5200_RS232_ExitSplitScreen( int nCardID );
CP5200API int       CALLING_CONV CP5200_RS232_SaveClearWndData( int nCardID , int nSavaOrClear );
CP5200API int		CALLING_CONV CP5200_RS232_PlaySelectedPrg(int nCardID, const WORD *pSelected, int nSelCnt, int nOption);
CP5200API int		CALLING_CONV CP5200_RS232_SetUserVarData(int nCardID, int bSave , int nVarNum , int bAstride ,  int* nWarLen , BYTE* byNoData );
CP5200API int		CALLING_CONV CP5200_RS232_SetSelectedAndUserVarData(int nCardID, int bSave , int nVarNum , int bAstride ,  int* nWarLen , BYTE* byNoData, int nSelPrg );
CP5200API int       CALLING_CONV CP5200_RS232_SetGlobalZone(int nCardID, BYTE byConfig , BYTE bySynchro , BYTE byZoneNum  ,  BYTE* pZoneMsg );
CP5200API int       CALLING_CONV CP5200_RS232_PushUserVarData(int nCardID, BYTE byOption , BYTE byVarZoonNum , BYTE byVarDataLen  ,  BYTE* pVarNoAndData );
CP5200API int       CALLING_CONV CP5200_RS232_TimerCtrl(int nCardID, BYTE byTimerNo , BYTE byCmd  , BYTE byProp , DWORD dwValue  );



//节目模板
CP5200API int		CALLING_CONV CPowerBox_RS232_SetProgramTemplate(int nCardID, BYTE byColor ,USHORT nWidth , USHORT nHeight , BYTE nWndNum , BYTE* pDefParam , BYTE* pWndParam);
CP5200API int		CALLING_CONV CPowerBox_RS232_InOutProgramTemplate( int nCardID,BYTE byInOrOut );
CP5200API int		CALLING_CONV CPowerBox_RS232_QueryProgramTemplate(int nCardID , BYTE* pState );
CP5200API int		CALLING_CONV CPowerBox_RS232_DeleteProgram( int nCardID,BYTE byConfig , BYTE byProNum , BYTE* pDelPro );
CP5200API int       CALLING_CONV CPowerBox_RS232_SendText( int nCardID, DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byProp , BYTE* pShowFormat , char* pText);
CP5200API int		CALLING_CONV CPowerBox_RS232_SendPicture( int nCardID, DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byPicType , BYTE* pShowFormat, BYTE* pPicData , long lPicDataLen);
CP5200API int		CALLING_CONV CPowerBox_RS232_SetAloneProgram(int nCardID,DWORD dwAppendCode , BYTE byProgramNo , BYTE byWindowCnt ,BYTE* pWndParam, BYTE* pWndData);
CP5200API int		CALLING_CONV CPowerBox_RS232_SetProgramProperty( int nCardID, BYTE byOption , BYTE byProgramCnt , BYTE* pPrograms , BYTE byPropertyID1 , BYTE byPropertyID2 , BYTE byProgramLevel , USHORT nLoopCnt , USHORT nTime , BYTE* pDuetime , BYTE* pTimeInterval);



//Network
CP5200API int		CALLING_CONV CP5200_Net_UploadFile(int nCardID, const char* pSourceFilename, const char *pTargetFilename);
CP5200API int		CALLING_CONV CP5200_Net_TestController(int nCardID);
CP5200API int		CALLING_CONV CP5200_Net_TestCommunication(int nCardID);
CP5200API int		CALLING_CONV CP5200_Net_GetTime(int nCardID, BYTE *pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_Net_SetTime(BYTE nCardID, const BYTE *pInfo);
CP5200API int		CALLING_CONV CP5200_Net_GetTempHumi(int nCardID, BYTE byFlag , BYTE *pBuf, int nBufSize  );
CP5200API int		CALLING_CONV CP5200_Net_RestartApp(BYTE nCardID);
CP5200API int		CALLING_CONV CP5200_Net_RestartSys(BYTE nCardID);
CP5200API int		CALLING_CONV CP5200_Net_GetTypeInfo(BYTE nCardID, BYTE *pBuf, int nBufSize);
CP5200API int		CALLING_CONV CP5200_Net_SendInstantMessage( BYTE nCardID, BYTE byPlayTimes , int x  , int y , int cx , int cy , BYTE byFontSizeColor , int nEffect , BYTE nSpeed , BYTE byStayTime ,const char* pText ) ;

CP5200API int		CALLING_CONV CP5200_Net_ReadHWSetting(BYTE nCardID, BYTE *pBuf, int nBufSize , int nPassword);
CP5200API int		CALLING_CONV CP5200_Net_WriteHWSetting(BYTE nCardID,const BYTE *pSetting,  int nPassword);

CP5200API int		CALLING_CONV CP5200_Net_SplitScreen(int nCardID, int nScrWidth, int nScrHeight, int nWndCnt, const int *pWndRects);
CP5200API int		CALLING_CONV CP5200_Net_SendText(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_Net_SendTagText(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nSpeed, int nEffect, int nStayTime, int nAlignment);
CP5200API int		CALLING_CONV CP5200_Net_SendPicture(int nCardID, int nWndNo, int nPosX, int nPosY, int nCx, int nCy, const char *pPictureFile, int nSpeed, int nEffect, int nStayTime, int nPictRef);
CP5200API int		CALLING_CONV CP5200_Net_SendStatic(int nCardID, int nWndNo, const char *pText, COLORREF crColor, int nFontSize, int nAlignment, int x, int y, int cx, int cy);
CP5200API int       CALLING_CONV CP5200_Net_SendClock( int nCardID, int nWinNo , int nStayTime , int nCalendar , int nFormat , int nContent , int nFont , int nRed , int nGreen , int nBlue ,  LPCSTR pTxt );
CP5200API int       CALLING_CONV CP5200_Net_ExitSplitScreen( int nCardID );
CP5200API int       CALLING_CONV CP5200_Net_SaveClearWndData( int nCardID , int nSavaOrClear );
CP5200API int		CALLING_CONV CP5200_Net_PlaySelectedPrg(int nCardID, const WORD *pSelected, int nSelCnt, int nOption);
CP5200API int		CALLING_CONV CP5200_Net_SetUserVarData(int nCardID, int bSave , int nVarNum , int bAstride ,  int* nWarLen , BYTE* byNoData );
CP5200API int		CALLING_CONV CP5200_Net_SetSelectedAndUserVarData(int nCardID, int bSave , int nVarNum , int bAstride ,  int* nWarLen , BYTE* byNoData, int nSelPrg );
CP5200API int       CALLING_CONV CP5200_Net_SetGlobalZone(int nCardID, BYTE byConfig , BYTE bySynchro , BYTE byZoneNum  , BYTE* pZoneMsg );
CP5200API int       CALLING_CONV CP5200_Net_PushUserVarData(int nCardID, BYTE byOption , BYTE byVarZoonNum , BYTE byVarDataLen  ,  BYTE* pVarNoAndData );
CP5200API int       CALLING_CONV CP5200_Net_TimerCtrl(int nCardID, BYTE byTimerNo , BYTE byCmd  , BYTE byProp , DWORD dwValue );


//节目模板
CP5200API int		CALLING_CONV CPowerBox_Net_SetProgramTemplate(int nCardID, BYTE byColor ,USHORT nWidth , USHORT nHeight , BYTE nWndNum , BYTE* pDefParam , BYTE* pWndParam);
CP5200API int		CALLING_CONV CPowerBox_Net_InOutProgramTemplate( int nCardID,BYTE byInOrOut );
CP5200API int		CALLING_CONV CPowerBox_Net_QueryProgramTemplate(int nCardID , BYTE* pState );
CP5200API int		CALLING_CONV CPowerBox_Net_DeleteProgram( int nCardID,BYTE byConfig , BYTE byProNum , BYTE* pDelPro );
CP5200API int       CALLING_CONV CPowerBox_Net_SendText( int nCardID, DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byProp , BYTE* pShowFormat , char* pText);
CP5200API int		CALLING_CONV CPowerBox_Net_SendPicture( int nCardID, DWORD dwAppendCode , BYTE byProNo , BYTE byWndNo , BYTE byPicType , BYTE* pShowFormat, BYTE* pPicData , long lPicDataLen);
CP5200API int		CALLING_CONV CPowerBox_Net_SetAloneProgram(int nCardID,DWORD dwAppendCode , BYTE byProgramNo , BYTE byWindowCnt ,BYTE* pWndParam, BYTE* pWndData);
CP5200API int		CALLING_CONV CPowerBox_Net_SetProgramProperty( int nCardID, BYTE byOption , BYTE byProgramCnt , BYTE* pPrograms , BYTE byPropertyID1 , BYTE byPropertyID2 , BYTE byProgramLevel , USHORT nLoopCnt , USHORT nTime , BYTE* pDuetime , BYTE* pTimeInterval);

//====================================================================================
// API for running schedule
//====================================================================================
CP5200API HOBJECT	CALLING_CONV CP5200_Runsch_Create(int nPrgSum, int nAttrib);
CP5200API int		CALLING_CONV CP5200_Runsch_Destroy(HOBJECT hObj);
CP5200API int		CALLING_CONV CP5200_Runsch_AddItem(HOBJECT hObj, int nGrade, int nWeekDateRelative, int nWeeks, 
													   const int* pBeginDate, const int* pEndDate, 
													   const int* pBeginTime, const int* pEndTime, 
													   int nItemCnt, const int *pItems);
CP5200API int		CALLING_CONV CP5200_Runsch_SaveToFile(HOBJECT hObj, const char* pFilename);

//====================================================================================
// miscellaneous APIs
//====================================================================================
CP5200API int		CALLING_CONV CP5200_CalcImageDataSize(WORD imgw, WORD imgh, BYTE color);
CP5200API int		CALLING_CONV CP5200_MakeImageDataFromFile(WORD imgw, WORD imgh, BYTE color, BYTE *pDatBuf, int nBufSize, const char* pFilename, int nMode);


#endif //_CP5200API_H_

