using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000150 RID: 336
	public sealed class Clipboard
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00002843 File Offset: 0x00000A43
		private Clipboard()
		{
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00026891 File Offset: 0x00024A91
		private static bool IsFormatValid(DataObject data)
		{
			return Clipboard.IsFormatValid(data.GetFormats());
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000268A0 File Offset: 0x00024AA0
		internal static bool IsFormatValid(string[] formats)
		{
			if (formats != null && formats.Length <= 4)
			{
				foreach (string a in formats)
				{
					if (!(a == "Text") && !(a == "UnicodeText") && !(a == "System.String") && !(a == "Csv"))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00026904 File Offset: 0x00024B04
		internal static bool IsFormatValid(FORMATETC[] formats)
		{
			if (formats != null && formats.Length <= 4)
			{
				for (int i = 0; i < formats.Length; i++)
				{
					short cfFormat = formats[i].cfFormat;
					if (cfFormat != 1 && cfFormat != 13 && (int)cfFormat != DataFormats.GetFormat("System.String").Id && (int)cfFormat != DataFormats.GetFormat("Csv").Id)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00026967 File Offset: 0x00024B67
		public static void SetDataObject(object data)
		{
			Clipboard.SetDataObject(data, false);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00026970 File Offset: 0x00024B70
		public static void SetDataObject(object data, bool copy)
		{
			Clipboard.SetDataObject(data, copy, 10, 100);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00026980 File Offset: 0x00024B80
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public static void SetDataObject(object data, bool copy, int retryTimes, int retryDelay)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (retryTimes < 0)
			{
				throw new ArgumentOutOfRangeException("retryTimes", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"retryTimes",
					retryTimes.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (retryDelay < 0)
			{
				throw new ArgumentOutOfRangeException("retryDelay", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"retryDelay",
					retryDelay.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			DataObject dataObject = null;
			if (!(data is IDataObject))
			{
				dataObject = new DataObject(data);
			}
			bool flag = false;
			try
			{
				IntSecurity.ClipboardRead.Demand();
			}
			catch (SecurityException)
			{
				flag = true;
			}
			if (flag)
			{
				if (dataObject == null)
				{
					dataObject = (data as DataObject);
				}
				if (!Clipboard.IsFormatValid(dataObject))
				{
					throw new SecurityException(SR.GetString("ClipboardSecurityException"));
				}
			}
			if (dataObject != null)
			{
				dataObject.RestrictedFormats = flag;
			}
			int num = retryTimes;
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				int num2;
				do
				{
					if (data is IDataObject)
					{
						num2 = UnsafeNativeMethods.OleSetClipboard((IDataObject)data);
					}
					else
					{
						num2 = UnsafeNativeMethods.OleSetClipboard(dataObject);
					}
					if (num2 != 0)
					{
						if (num == 0)
						{
							Clipboard.ThrowIfFailed(num2);
						}
						num--;
						Thread.Sleep(retryDelay);
					}
				}
				while (num2 != 0);
				if (copy)
				{
					num = retryTimes;
					do
					{
						num2 = UnsafeNativeMethods.OleFlushClipboard();
						if (num2 != 0)
						{
							if (num == 0)
							{
								Clipboard.ThrowIfFailed(num2);
							}
							num--;
							Thread.Sleep(retryDelay);
						}
					}
					while (num2 != 0);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00026B20 File Offset: 0x00024D20
		public static IDataObject GetDataObject()
		{
			IntSecurity.ClipboardRead.Demand();
			if (Application.OleRequired() == ApartmentState.STA)
			{
				return Clipboard.GetDataObject(10, 100);
			}
			if (Application.MessageLoop)
			{
				throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
			}
			return null;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00026B58 File Offset: 0x00024D58
		private static IDataObject GetDataObject(int retryTimes, int retryDelay)
		{
			IDataObject dataObject = null;
			int num = retryTimes;
			int num2;
			do
			{
				num2 = UnsafeNativeMethods.OleGetClipboard(ref dataObject);
				if (num2 != 0)
				{
					if (num == 0)
					{
						Clipboard.ThrowIfFailed(num2);
					}
					num--;
					Thread.Sleep(retryDelay);
				}
			}
			while (num2 != 0);
			if (dataObject == null)
			{
				return null;
			}
			if (dataObject is IDataObject && !Marshal.IsComObject(dataObject))
			{
				return (IDataObject)dataObject;
			}
			return new DataObject(dataObject);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00026BAC File Offset: 0x00024DAC
		public static void Clear()
		{
			Clipboard.SetDataObject(new DataObject());
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public static bool ContainsAudio()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject != null && dataObject.GetDataPresent(DataFormats.WaveAudio, false);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00026BDC File Offset: 0x00024DDC
		public static bool ContainsData(string format)
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject != null && dataObject.GetDataPresent(format, false);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00026BFC File Offset: 0x00024DFC
		public static bool ContainsFileDropList()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop, true);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00026C20 File Offset: 0x00024E20
		public static bool ContainsImage()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject != null && dataObject.GetDataPresent(DataFormats.Bitmap, true);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00026C44 File Offset: 0x00024E44
		public static bool ContainsText()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				return Clipboard.ContainsText(TextDataFormat.Text);
			}
			return Clipboard.ContainsText(TextDataFormat.UnicodeText);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00026C74 File Offset: 0x00024E74
		public static bool ContainsText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject != null && dataObject.GetDataPresent(Clipboard.ConvertToDataFormats(format), false);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public static Stream GetAudioStream()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject != null)
			{
				return dataObject.GetData(DataFormats.WaveAudio, false) as Stream;
			}
			return null;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00026CEC File Offset: 0x00024EEC
		public static object GetData(string format)
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject != null)
			{
				return dataObject.GetData(format);
			}
			return null;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00026D0C File Offset: 0x00024F0C
		public static StringCollection GetFileDropList()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			StringCollection stringCollection = new StringCollection();
			if (dataObject != null)
			{
				string[] array = dataObject.GetData(DataFormats.FileDrop, true) as string[];
				if (array != null)
				{
					stringCollection.AddRange(array);
				}
			}
			return stringCollection;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00026D48 File Offset: 0x00024F48
		public static Image GetImage()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject != null)
			{
				return dataObject.GetData(DataFormats.Bitmap, true) as Image;
			}
			return null;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00026D71 File Offset: 0x00024F71
		public static string GetText()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				return Clipboard.GetText(TextDataFormat.Text);
			}
			return Clipboard.GetText(TextDataFormat.UnicodeText);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00026DA0 File Offset: 0x00024FA0
		public static string GetText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject != null)
			{
				string text = dataObject.GetData(Clipboard.ConvertToDataFormats(format), false) as string;
				if (text != null)
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00026DF9 File Offset: 0x00024FF9
		public static void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			Clipboard.SetAudio(new MemoryStream(audioBytes));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00026E14 File Offset: 0x00025014
		public static void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			IDataObject dataObject = new DataObject();
			dataObject.SetData(DataFormats.WaveAudio, false, audioStream);
			Clipboard.SetDataObject(dataObject, true);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00026E4C File Offset: 0x0002504C
		public static void SetData(string format, object data)
		{
			IDataObject dataObject = new DataObject();
			dataObject.SetData(format, data);
			Clipboard.SetDataObject(dataObject, true);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00026E70 File Offset: 0x00025070
		public static void SetFileDropList(StringCollection filePaths)
		{
			if (filePaths == null)
			{
				throw new ArgumentNullException("filePaths");
			}
			if (filePaths.Count == 0)
			{
				throw new ArgumentException(SR.GetString("CollectionEmptyException"));
			}
			foreach (string text in filePaths)
			{
				try
				{
					string fullPath = Path.GetFullPath(text);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					throw new ArgumentException(SR.GetString("Clipboard_InvalidPath", new object[]
					{
						text,
						"filePaths"
					}), ex);
				}
			}
			if (filePaths.Count > 0)
			{
				IDataObject dataObject = new DataObject();
				string[] array = new string[filePaths.Count];
				filePaths.CopyTo(array, 0);
				dataObject.SetData(DataFormats.FileDrop, true, array);
				Clipboard.SetDataObject(dataObject, true);
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00026F64 File Offset: 0x00025164
		public static void SetImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			IDataObject dataObject = new DataObject();
			dataObject.SetData(DataFormats.Bitmap, true, image);
			Clipboard.SetDataObject(dataObject, true);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00026F99 File Offset: 0x00025199
		public static void SetText(string text)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				Clipboard.SetText(text, TextDataFormat.Text);
				return;
			}
			Clipboard.SetText(text, TextDataFormat.UnicodeText);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00026FCC File Offset: 0x000251CC
		public static void SetText(string text, TextDataFormat format)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			IDataObject dataObject = new DataObject();
			dataObject.SetData(Clipboard.ConvertToDataFormats(format), false, text);
			Clipboard.SetDataObject(dataObject, true);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00027030 File Offset: 0x00025230
		private static string ConvertToDataFormats(TextDataFormat format)
		{
			switch (format)
			{
			case TextDataFormat.Text:
				return DataFormats.Text;
			case TextDataFormat.UnicodeText:
				return DataFormats.UnicodeText;
			case TextDataFormat.Rtf:
				return DataFormats.Rtf;
			case TextDataFormat.Html:
				return DataFormats.Html;
			case TextDataFormat.CommaSeparatedValue:
				return DataFormats.CommaSeparatedValue;
			default:
				return DataFormats.UnicodeText;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002707C File Offset: 0x0002527C
		private static void ThrowIfFailed(int hr)
		{
			if (hr != 0)
			{
				ExternalException ex = new ExternalException(SR.GetString("ClipboardOperationFailed"), hr);
				throw ex;
			}
		}
	}
}
