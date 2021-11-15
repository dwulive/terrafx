// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using TerraFX.Interop.Xlib;

namespace TerraFX.UI
{
    internal enum XlibRequestCode
    {
        CreateWindow = Xlib.X_CreateWindow,
        ChangeWindowAttributes = Xlib.X_ChangeWindowAttributes,
        GetWindowAttributes = Xlib.X_GetWindowAttributes,
        DestroyWindow = Xlib.X_DestroyWindow,
        DestroySubwindows = Xlib.X_DestroySubwindows,
        ChangeSaveSet = Xlib.X_ChangeSaveSet,
        ReparentWindow = Xlib.X_ReparentWindow,
        MapWindow = Xlib.X_MapWindow,
        MapSubwindows = Xlib.X_MapSubwindows,
        UnmapWindow = Xlib.X_UnmapWindow,
        UnmapSubwindows = Xlib.X_UnmapSubwindows,
        ConfigureWindow = Xlib.X_ConfigureWindow,
        CirculateWindow = Xlib.X_CirculateWindow,
        GetGeometry = Xlib.X_GetGeometry,
        QueryTree = Xlib.X_QueryTree,
        InternAtom = Xlib.X_InternAtom,
        GetAtomName = Xlib.X_GetAtomName,
        ChangeProperty = Xlib.X_ChangeProperty,
        DeleteProperty = Xlib.X_DeleteProperty,
        GetProperty = Xlib.X_GetProperty,
        ListProperties = Xlib.X_ListProperties,
        SetSelectionOwner = Xlib.X_SetSelectionOwner,
        GetSelectionOwner = Xlib.X_GetSelectionOwner,
        ConvertSelection = Xlib.X_ConvertSelection,
        SendEvent = Xlib.X_SendEvent,
        GrabPointer = Xlib.X_GrabPointer,
        UngrabPointer = Xlib.X_UngrabPointer,
        GrabButton = Xlib.X_GrabButton,
        UngrabButton = Xlib.X_UngrabButton,
        ChangeActivePointerGrab = Xlib.X_ChangeActivePointerGrab,
        GrabKeyboard = Xlib.X_GrabKeyboard,
        UngrabKeyboard = Xlib.X_UngrabKeyboard,
        GrabKey = Xlib.X_GrabKey,
        UngrabKey = Xlib.X_UngrabKey,
        AllowEvents = Xlib.X_AllowEvents,
        GrabServer = Xlib.X_GrabServer,
        UngrabServer = Xlib.X_UngrabServer,
        QueryPointer = Xlib.X_QueryPointer,
        GetMotionEvents = Xlib.X_GetMotionEvents,
        TranslateCoords = Xlib.X_TranslateCoords,
        WarpPointer = Xlib.X_WarpPointer,
        SetInputFocus = Xlib.X_SetInputFocus,
        GetInputFocus = Xlib.X_GetInputFocus,
        QueryKeymap = Xlib.X_QueryKeymap,
        OpenFont = Xlib.X_OpenFont,
        CloseFont = Xlib.X_CloseFont,
        QueryFont = Xlib.X_QueryFont,
        QueryTextExtents = Xlib.X_QueryTextExtents,
        ListFonts = Xlib.X_ListFonts,
        ListFontsWithInfo = Xlib.X_ListFontsWithInfo,
        SetFontPath = Xlib.X_SetFontPath,
        GetFontPath = Xlib.X_GetFontPath,
        CreatePixmap = Xlib.X_CreatePixmap,
        FreePixmap = Xlib.X_FreePixmap,
        CreateGC = Xlib.X_CreateGC,
        ChangeGC = Xlib.X_ChangeGC,
        CopyGC = Xlib.X_CopyGC,
        SetDashes = Xlib.X_SetDashes,
        SetClipRectangles = Xlib.X_SetClipRectangles,
        FreeGC = Xlib.X_FreeGC,
        ClearArea = Xlib.X_ClearArea,
        CopyArea = Xlib.X_CopyArea,
        CopyPlane = Xlib.X_CopyPlane,
        PolyPoint = Xlib.X_PolyPoint,
        PolyLine = Xlib.X_PolyLine,
        PolySegment = Xlib.X_PolySegment,
        PolyRectangle = Xlib.X_PolyRectangle,
        PolyArc = Xlib.X_PolyArc,
        FillPoly = Xlib.X_FillPoly,
        PolyFillRectangle = Xlib.X_PolyFillRectangle,
        PolyFillArc = Xlib.X_PolyFillArc,
        PutImage = Xlib.X_PutImage,
        GetImage = Xlib.X_GetImage,
        PolyText8 = Xlib.X_PolyText8,
        PolyText16 = Xlib.X_PolyText16,
        ImageText8 = Xlib.X_ImageText8,
        ImageText16 = Xlib.X_ImageText16,
        CreateColormap = Xlib.X_CreateColormap,
        FreeColormap = Xlib.X_FreeColormap,
        CopyColormapAndFree = Xlib.X_CopyColormapAndFree,
        InstallColormap = Xlib.X_InstallColormap,
        UninstallColormap = Xlib.X_UninstallColormap,
        ListInstalledColormaps = Xlib.X_ListInstalledColormaps,
        AllocColor = Xlib.X_AllocColor,
        AllocNamedColor = Xlib.X_AllocNamedColor,
        AllocColorCells = Xlib.X_AllocColorCells,
        AllocColorPlanes = Xlib.X_AllocColorPlanes,
        FreeColors = Xlib.X_FreeColors,
        StoreColors = Xlib.X_StoreColors,
        StoreNamedColor = Xlib.X_StoreNamedColor,
        QueryColors = Xlib.X_QueryColors,
        LookupColor = Xlib.X_LookupColor,
        CreateCursor = Xlib.X_CreateCursor,
        CreateGlyphCursor = Xlib.X_CreateGlyphCursor,
        FreeCursor = Xlib.X_FreeCursor,
        RecolorCursor = Xlib.X_RecolorCursor,
        QueryBestSize = Xlib.X_QueryBestSize,
        QueryExtension = Xlib.X_QueryExtension,
        ListExtensions = Xlib.X_ListExtensions,
        ChangeKeyboardMapping = Xlib.X_ChangeKeyboardMapping,
        GetKeyboardMapping = Xlib.X_GetKeyboardMapping,
        ChangeKeyboardControl = Xlib.X_ChangeKeyboardControl,
        GetKeyboardControl = Xlib.X_GetKeyboardControl,
        Bell = Xlib.X_Bell,
        ChangePointerControl = Xlib.X_ChangePointerControl,
        GetPointerControl = Xlib.X_GetPointerControl,
        SetScreenSaver = Xlib.X_SetScreenSaver,
        GetScreenSaver = Xlib.X_GetScreenSaver,
        ChangeHosts = Xlib.X_ChangeHosts,
        ListHosts = Xlib.X_ListHosts,
        SetAccessControl = Xlib.X_SetAccessControl,
        SetCloseDownMode = Xlib.X_SetCloseDownMode,
        KillClient = Xlib.X_KillClient,
        RotateProperties = Xlib.X_RotateProperties,
        ForceScreenSaver = Xlib.X_ForceScreenSaver,
        SetPointerMapping = Xlib.X_SetPointerMapping,
        GetPointerMapping = Xlib.X_GetPointerMapping,
        SetModifierMapping = Xlib.X_SetModifierMapping,
        GetModifierMapping = Xlib.X_GetModifierMapping,
        NoOperation = Xlib.X_NoOperation,
    }
}
