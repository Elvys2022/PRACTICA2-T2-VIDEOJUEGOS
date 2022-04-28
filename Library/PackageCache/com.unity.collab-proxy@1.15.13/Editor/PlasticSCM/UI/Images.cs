﻿using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

using Codice.LogWrapper;
using PlasticGui.Help;
using Unity.PlasticSCM.Editor.AssetUtils;

namespace Unity.PlasticSCM.Editor.UI
{
    internal class Images
    {
        internal enum Name
        {
            None,

            IconPlastic,
            IconCloseButton,
            IconPressedCloseButton,
            IconAdded,
            IconAddedLocal,
            IconAddedOverlay,
            IconPrivateOverlay,
            IconCheckedOutLocalOverlay,
            IconDeleted,
            IconDeletedLocalOverlay,
            IconDeletedRemote,
            IconDeletedRemoteOverlay,
            IconChanged,
            IconOutOfSync,
            IconOutOfSyncOverlay,
            IconMoved,
            IconMergeLink,
            Ignored,
            IgnoredOverlay,
            IconConflicted,
            IconMergeConflict,
            IconConflictedOverlay,
            IconConflictResolvedOverlay,
            IconLockedLocalOverlay,
            IconLockedRemoteOverlay,
            IconMerged,
            IconFsChanged,
            IconMergeCategory,
            XLink,
            Ok,
            SecondaryTabClose,
            SecondaryTabCloseHover,
            NotOnDisk,
            IconRepository,
            IconPlasticView,
            IconPlasticViewNotify,
            IconPlasticNotifyIncoming,
            IconPlasticNotifyConflict,
            Loading,
            IconEmptyGravatar,
            Step1,
            Step2,
            Step3,
            StepOk,
            ButtonSsoSignInUnity,
            ButtonSsoSignInEmail,
            ButtonSsoSignInGoogle,
            IconBranch,
            IconUndo,
            Refresh
        }

        internal static Texture2D GetHelpImage(HelpImage image)
        {
            // We use the dark version for both the light/dark skins since it matches the grey background better
            string helpImageFileName = string.Format(
                "d_{0}.png",
                HelpImageName.FromHelpImage(image));

            string imageRelativePath = GetImageFileRelativePath(helpImageFileName);
            Texture2D result = TryLoadImage(imageRelativePath, imageRelativePath);

            if (result != null)
                return result;

            mLog.WarnFormat("Image not found: {0}", helpImageFileName);
            return GetEmptyImage();
        }

        internal static Texture2D GetImage(Name image)
        {
            string imageFileName = image.ToString().ToLower() + ".png";
            string imageFileName2x = image.ToString().ToLower() + "@2x.png";

            string darkImageFileName = string.Format("d_{0}", imageFileName);
            string darkImageFileName2x = string.Format("d_{0}", imageFileName2x);

            string imageFileRelativePath = GetImageFileRelativePath(imageFileName);
            string imageFileRelativePath2x = GetImageFileRelativePath(imageFileName2x);

            string darkImageFileRelativePath = GetImageFileRelativePath(darkImageFileName);
            string darkImageFileRelativePath2x = GetImageFileRelativePath(darkImageFileName2x);

            Texture2D result = null;

            if (EditorGUIUtility.isProSkin)
                result = TryLoadImage(darkImageFileRelativePath, darkImageFileRelativePath2x);

            if (result != null)
                return result;

            result = TryLoadImage(imageFileRelativePath, imageFileRelativePath2x);

            if (result != null)
                return result;

            mLog.WarnFormat("Image not found: {0}", imageFileName);
            return GetEmptyImage();
        }

        internal static Texture GetFileIcon(string path)
        {
            string relativePath = GetRelativePath.ToApplication(path);

            return GetFileIconFromRelativePath(relativePath);
        }

        internal static Texture GetFileIconFromCmPath(string path)
        {
            return GetFileIconFromRelativePath(
                path.Substring(1).Replace("/",
                Path.DirectorySeparatorChar.ToString()));
        }

        internal static Texture GetDropDownIcon()
        {
            return GetIconFromEditorGUI("icon dropdown");
        }

        internal static Texture GetDirectoryIcon()
        {
            return GetIconFromEditorGUI("Folder Icon");
        }

        internal static Texture GetPrivatedOverlayIcon()
        {
            if (mPrivatedOverlayIcon == null)
                mPrivatedOverlayIcon = GetImage(Name.IconPrivateOverlay);

            return mPrivatedOverlayIcon;
        }

        internal static Texture GetAddedOverlayIcon()
        {
            if (mAddedOverlayIcon == null)
                mAddedOverlayIcon = GetImage(Name.IconAddedOverlay);

            return mAddedOverlayIcon;
        }

        internal static Texture GetDeletedLocalOverlayIcon()
        {
            if (mDeletedLocalOverlayIcon == null)
                mDeletedLocalOverlayIcon = GetImage(Name.IconDeletedLocalOverlay);

            return mDeletedLocalOverlayIcon;
        }

        internal static Texture GetDeletedRemoteOverlayIcon()
        {
            if (mDeletedRemoteOverlayIcon == null)
                mDeletedRemoteOverlayIcon = GetImage(Name.IconDeletedRemoteOverlay);

            return mDeletedRemoteOverlayIcon;
        }

        internal static Texture GetCheckedOutOverlayIcon()
        {
            if (mCheckedOutOverlayIcon == null)
                mCheckedOutOverlayIcon = GetImage(Name.IconCheckedOutLocalOverlay);

            return mCheckedOutOverlayIcon;
        }

        internal static Texture GetOutOfSyncOverlayIcon()
        {
            if (mOutOfSyncOverlayIcon == null)
                mOutOfSyncOverlayIcon = GetImage(Name.IconOutOfSyncOverlay);

            return mOutOfSyncOverlayIcon;
        }

        internal static Texture GetConflictedOverlayIcon()
        {
            if (mConflictedOverlayIcon == null)
                mConflictedOverlayIcon = GetImage(Name.IconConflictedOverlay);

            return mConflictedOverlayIcon;
        }

        internal static Texture GetConflictResolvedOverlayIcon()
        {
            if (mConflictResolvedOverlayIcon == null)
                mConflictResolvedOverlayIcon = GetImage(Name.IconConflictResolvedOverlay);

            return mConflictResolvedOverlayIcon;
        }

        internal static Texture GetLockedLocalOverlayIcon()
        {
            if (mLockedLocalOverlayIcon == null)
                mLockedLocalOverlayIcon = GetImage(Name.IconLockedLocalOverlay);

            return mLockedLocalOverlayIcon;
        }

        internal static Texture GetLockedRemoteOverlayIcon()
        {
            if (mLockedRemoteOverlayIcon == null)
                mLockedRemoteOverlayIcon = GetImage(Name.IconLockedRemoteOverlay);

            return mLockedRemoteOverlayIcon;
        }

        internal static Texture GetIgnoredOverlayIcon()
        {
            if (mIgnoredverlayIcon == null)
                mIgnoredverlayIcon = GetImage(Name.IgnoredOverlay);

            return mIgnoredverlayIcon;
        }

        internal static Texture GetWarnIcon()
        {
            return GetIconFromEditorGUI("console.warnicon.sml");
        }

        internal static Texture GetInfoIcon()
        {
            return GetIconFromEditorGUI("console.infoicon.sml");
        }

        internal static Texture GetErrorDialogIcon()
        {
            return GetIconFromEditorGUI("console.erroricon");
        }

        internal static Texture GetWarnDialogIcon()
        {
            return GetIconFromEditorGUI("console.warnicon");
        }

        internal static Texture GetInfoDialogIcon()
        {
            return GetIconFromEditorGUI("console.infoicon");
        }

        internal static Texture GetRefreshIcon()
        {
            if (mRefreshIcon == null)
                mRefreshIcon = GetImage(Name.Refresh);

            return mRefreshIcon;
        }

        internal static Texture GetSettingsIcon()
        {
            return GetIconFromEditorGUI("settings");
        }

        internal static Texture GetCloseIcon()
        {
            if (mCloseIcon == null)
                mCloseIcon = GetImage(Name.SecondaryTabClose);

            return mCloseIcon;
        }

        internal static Texture GetClickedCloseIcon()
        {
            if (mClickedCloseIcon == null)
                mClickedCloseIcon = GetImage(Name.SecondaryTabCloseHover);

            return mClickedCloseIcon;
        }

        internal static Texture GetHoveredCloseIcon()
        {
            if (mHoveredCloseIcon == null)
                mHoveredCloseIcon = GetImage(Name.SecondaryTabCloseHover);

            return mHoveredCloseIcon;
        }

        internal static Texture GetFileIcon()
        {
            if (mFileIcon == null)
                mFileIcon = EditorGUIUtility.FindTexture("DefaultAsset Icon");

            if (mFileIcon == null)
                mFileIcon = GetIconFromAssetPreview(typeof(DefaultAsset));

            if (mFileIcon == null)
                mFileIcon = GetEmptyImage();

            return mFileIcon;
        }

        internal static Texture2D GetLinkUnderlineImage()
        {
            if (mLinkUnderlineImage == null)
            {
                mLinkUnderlineImage = new Texture2D(1, 1);
                mLinkUnderlineImage.SetPixel(0, 0, UnityStyles.Colors.Link);
                mLinkUnderlineImage.Apply();
            }

            return mLinkUnderlineImage;
        }

        internal static Texture2D GetTreeviewBackgroundTexture()
        {
            if (mTreeviewBackgroundTexture == null)
                mTreeviewBackgroundTexture = GetTextureFromColor(UnityStyles.Colors.TreeViewBackground);

            return mTreeviewBackgroundTexture;
        }

        internal static Texture2D GetCommentBackgroundTexture()
        {
            if (mCommentBackground == null)
                mCommentBackground = GetTextureFromColor(UnityStyles.Colors.CommentsBackground);

            return mCommentBackground;
        }

        internal static Texture2D GetColumnsBackgroundTexture()
        {
            if (mColumnsBackgroundTexture == null)
                mColumnsBackgroundTexture = GetTextureFromColor(UnityStyles.Colors.ColumnsBackground);

            return mColumnsBackgroundTexture;
        }

        static Texture2D GetEmptyImage()
        {
            if (mEmptyImage == null)
                mEmptyImage = GetTextureFromColor(Color.clear);

            return mEmptyImage;
        }

        static Texture2D GetTextureFromColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);

            texture.SetPixel(0, 0, color);
            texture.Apply();

            return texture;
        }

        static Texture GetFileIconFromRelativePath(string relativePath)
        {
            Texture result = AssetDatabase.GetCachedIcon(relativePath);

            if (result != null)
                return result;

            result = GetFileIconFromKnownExtension(relativePath);

            if (result != null)
                return result;

            return GetFileIcon();
        }

        static Texture GetFileIconFromKnownExtension(string relativePath)
        {
            if (relativePath.EndsWith(UnityConstants.TREEVIEW_META_LABEL))
            {
                relativePath = relativePath.Substring(0,
                    relativePath.Length- UnityConstants.TREEVIEW_META_LABEL.Length);
            }

            string extension = Path.GetExtension(relativePath).ToLower();

            if (extension.Equals(".cs"))
                return GetIconFromEditorGUI("cs Script Icon");

            if (extension.Equals(".png") || extension.Equals(".jpg")
             || extension.Equals(".jpeg") || extension.Equals(".gif")
             || extension.Equals(".tga") || extension.Equals(".bmp")
             || extension.Equals(".tif") || extension.Equals(".tiff"))
                return GetIconFromEditorGUI("d_Texture Icon");

            if (extension.Equals(".mat"))
                return GetIconFromAssetPreview(typeof(UnityEngine.Material));

            if (extension.Equals(".fbx") || extension.Equals(".ma")
             || extension.Equals(".mb") || extension.Equals(".blend")
             || extension.Equals(".max") )
                return GetIconFromAssetPreview(typeof(UnityEngine.GameObject));

            if (extension.Equals(".wav") || extension.Equals(".mp3"))
                return GetIconFromAssetPreview(typeof(UnityEngine.AudioClip));

            if (extension.Equals(".anim"))
                return GetIconFromAssetPreview(typeof(UnityEngine.Animation));

            if (extension.Equals(".animator"))
                return GetIconFromAssetPreview(typeof(UnityEngine.Animator));

            if (extension.Equals(".shader"))
                return GetIconFromEditorGUI("d_Shader Icon");

            if (extension.Equals(".asset") && relativePath.StartsWith("ProjectSettings\\"))
                return GetIconFromEditorGUI("EditorSettings Icon");

            return null;
        }

        static Texture2D GetIconFromEditorGUI(string name)
        {
            Texture2D result;

            if (mImagesFromEditorGUICache.TryGetValue(name, out result))
                return result;

            result = EditorGUIUtility.IconContent(name).image as Texture2D;

            mImagesFromEditorGUICache.Add(name, result);

            return result;
        }

        static Texture2D GetIconFromAssetPreview(System.Type type)
        {
            Texture2D result;

            if (mImagesFromAssetPreviewCache.TryGetValue(type.ToString(), out result))
                return result;

            result = AssetPreview.GetMiniTypeThumbnail(type);

            mImagesFromAssetPreviewCache.Add(type.ToString(), result);

            return result;
        }

        static string GetImageFileRelativePath(string imageFileName)
        {
            return Path.Combine(
                AssetsPath.GetImagesFolderRelativePath(),
                imageFileName);
        }

        static Texture2D TryLoadImage(string imageFileRelativePath, string image2xFilePath)
        {
            if (EditorGUIUtility.pixelsPerPoint > 1f && File.Exists(image2xFilePath))
                return LoadTextureFromFile(image2xFilePath);

            if (File.Exists(Path.GetFullPath(imageFileRelativePath)))
                return LoadTextureFromFile(imageFileRelativePath);

            return null;
        }

        static Texture2D LoadTextureFromFile(string path)
        {
            Texture2D result;

            if (mImagesByPathCache.TryGetValue(path, out result))
                return result;

            byte[] fileData = File.ReadAllBytes(path);
            result = new Texture2D(1, 1);
            result.LoadImage(fileData); //auto-resizes the texture dimensions

            mImagesByPathCache.Add(path, result);

            return result;
        }

        static Dictionary<string, Texture2D> mImagesByPathCache =
            new Dictionary<string, Texture2D>();
        static Dictionary<string, Texture2D> mImagesFromEditorGUICache =
            new Dictionary<string, Texture2D>();
        static Dictionary<string, Texture2D> mImagesFromAssetPreviewCache =
            new Dictionary<string, Texture2D>();

        static Texture mFileIcon;

        static Texture mPrivatedOverlayIcon;
        static Texture mAddedOverlayIcon;
        static Texture mDeletedLocalOverlayIcon;
        static Texture mDeletedRemoteOverlayIcon;
        static Texture mCheckedOutOverlayIcon;
        static Texture mOutOfSyncOverlayIcon;
        static Texture mConflictedOverlayIcon;
        static Texture mConflictResolvedOverlayIcon;
        static Texture mLockedLocalOverlayIcon;
        static Texture mLockedRemoteOverlayIcon;
        static Texture mIgnoredverlayIcon;

        static Texture mRefreshIcon;

        static Texture mCloseIcon;
        static Texture mClickedCloseIcon;
        static Texture mHoveredCloseIcon;

        static Texture2D mLinkUnderlineImage;

        static Texture2D mEmptyImage;

        static Texture2D mTreeviewBackgroundTexture;
        static Texture2D mColumnsBackgroundTexture;
        static Texture2D mCommentBackground;

        static readonly ILog mLog = LogManager.GetLogger("Images");
    }
}