// LafDllAPI.h
//
#ifndef _LAFDLLAPI_H_
#define _LAFDLLAPI_H_

#ifdef LAFDLL_EXPORTS
#define LAFDLLAPI extern "C" __declspec(dllexport) 
#else
#define LAFDLLAPI extern "C" __declspec(dllimport)
#endif
#define CALLING_CONV __stdcall

typedef void* HOBJECT;
typedef void* HFRAME;

#define ERR_OBJ_INVALID			-1
#define ERR_MEMORY_FAILED		-2
#define ERR_PAGE_INVALID		-3
#define ERR_LESS_BUFSIZE		-4
#define ERR_FILE_OPEN			-5
#define ERR_FILE_CREATE			-6


LAFDLLAPI HOBJECT	CALLING_CONV LafMaker_Create(WORD width, WORD height, BYTE color);
LAFDLLAPI int		CALLING_CONV LafMaker_Destroy(HOBJECT hObj);
LAFDLLAPI int		CALLING_CONV LafMaker_Reset(HOBJECT hObj, WORD width, WORD height, BYTE color);
LAFDLLAPI HFRAME	CALLING_CONV LafMaker_AddFrame(HOBJECT hObj, int nTime);
LAFDLLAPI HFRAME	CALLING_CONV LafMaker_GetFrame(HOBJECT hObj, int nIndex);
LAFDLLAPI int		CALLING_CONV LafMaker_DeleteFrame(HOBJECT hObj, int nIndex);
LAFDLLAPI int		CALLING_CONV LafMaker_SaveToFile(HOBJECT hObj, LPCTSTR pFile);
LAFDLLAPI int		CALLING_CONV LafMaker_SaveToBuffer(HOBJECT hObj, void *pBuffer, int nBufSize);

LAFDLLAPI BOOL		CALLING_CONV LafFrame_AddImage(HFRAME hFrame, int x, int y, LPCTSTR pFile);

LAFDLLAPI HOBJECT	CALLING_CONV LafReader_Create(void);
LAFDLLAPI int		CALLING_CONV LafReader_Destroy(HOBJECT hObj);
LAFDLLAPI int		CALLING_CONV LafReader_Load(HOBJECT hObj, LPCTSTR pFile);
LAFDLLAPI int		CALLING_CONV LafReader_GetInfo(HOBJECT hObj, WORD *width, WORD *height, BYTE *color);
LAFDLLAPI int		CALLING_CONV LafReader_FrameCount(HOBJECT hObj);
LAFDLLAPI int		CALLING_CONV LafReader_CurrentFrameIndex(HOBJECT hObj);
LAFDLLAPI int		CALLING_CONV LafReader_GetFrameImage(HOBJECT hObj, int nIndex, CImage &image, int &nValue);

LAFDLLAPI HOBJECT	CALLING_CONV LafFile_CreateFile(LPCTSTR pFilename, WORD width, WORD height, BYTE color);
LAFDLLAPI int		CALLING_CONV LafFile_SetFileType(HOBJECT hObj, int nFileType);
LAFDLLAPI int		CALLING_CONV LafFile_FrameNew(HOBJECT hObj, int nTime);
LAFDLLAPI int		CALLING_CONV LafFile_FrameAddImage(HOBJECT hObj, int x, int y, LPCTSTR pFile);
LAFDLLAPI int		CALLING_CONV LafFile_FrameAddImgpt(HOBJECT hObj, int x, int y, void *pImgpt, const char* pTypeName);
LAFDLLAPI int		CALLING_CONV LafFile_FrameSave(HOBJECT hObj);
LAFDLLAPI int		CALLING_CONV LafFile_CloseFile(HOBJECT hObj);

LAFDLLAPI int		CALLING_CONV Laf_CreateFileFromAvi(LPCTSTR pLafFile, WORD width, WORD height, BYTE color, int nMode, int nOption, LPCSTR pAviFile);
LAFDLLAPI int		CALLING_CONV Laf_ConvertLafFile(LPCTSTR pLafFile, WORD width, WORD height, BYTE color, int nMode, int nOption, LPCSTR pSrcLafFile);

#endif //_LAFDLLAPI_H_
