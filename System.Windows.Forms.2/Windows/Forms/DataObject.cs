using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000224 RID: 548
	[ClassInterface(ClassInterfaceType.None)]
	public class DataObject : IDataObject, IDataObject
	{
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000A98B1 File Offset: 0x000A7AB1
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x000A98B9 File Offset: 0x000A7AB9
		internal bool RestrictedFormats { get; set; }

		// Token: 0x06002384 RID: 9092 RVA: 0x000A98C2 File Offset: 0x000A7AC2
		internal DataObject(IDataObject data)
		{
			this.innerData = data;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000A98D1 File Offset: 0x000A7AD1
		internal DataObject(IDataObject data)
		{
			if (data is DataObject)
			{
				this.innerData = (data as IDataObject);
				return;
			}
			this.innerData = new DataObject.OleConverter(data);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000A98FA File Offset: 0x000A7AFA
		public DataObject()
		{
			this.innerData = new DataObject.DataStore();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000A9910 File Offset: 0x000A7B10
		public DataObject(object data)
		{
			if (data is IDataObject && !Marshal.IsComObject(data))
			{
				this.innerData = (IDataObject)data;
				return;
			}
			if (data is IDataObject)
			{
				this.innerData = new DataObject.OleConverter((IDataObject)data);
				return;
			}
			this.innerData = new DataObject.DataStore();
			this.SetData(data);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000A996C File Offset: 0x000A7B6C
		public DataObject(string format, object data) : this()
		{
			this.SetData(format, data);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000A997C File Offset: 0x000A7B7C
		private IntPtr GetCompatibleBitmap(Bitmap bm)
		{
			IntPtr hbitmap = bm.GetHbitmap();
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			IntPtr handle = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(null, handle), new HandleRef(bm, hbitmap));
			IntPtr handle3 = UnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, dc));
			IntPtr intPtr = SafeNativeMethods.CreateCompatibleBitmap(new HandleRef(null, dc), bm.Size.Width, bm.Size.Height);
			IntPtr handle4 = SafeNativeMethods.SelectObject(new HandleRef(null, handle3), new HandleRef(null, intPtr));
			SafeNativeMethods.BitBlt(new HandleRef(null, handle3), 0, 0, bm.Size.Width, bm.Size.Height, new HandleRef(null, handle), 0, 0, 13369376);
			SafeNativeMethods.SelectObject(new HandleRef(null, handle), new HandleRef(null, handle2));
			SafeNativeMethods.SelectObject(new HandleRef(null, handle3), new HandleRef(null, handle4));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, handle));
			UnsafeNativeMethods.DeleteCompatibleDC(new HandleRef(null, handle3));
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			SafeNativeMethods.DeleteObject(new HandleRef(bm, hbitmap));
			return intPtr;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000A9AAF File Offset: 0x000A7CAF
		public virtual object GetData(string format, bool autoConvert)
		{
			return this.innerData.GetData(format, autoConvert);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000A9ABE File Offset: 0x000A7CBE
		public virtual object GetData(string format)
		{
			return this.GetData(format, true);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000A9AC8 File Offset: 0x000A7CC8
		public virtual object GetData(Type format)
		{
			if (format == null)
			{
				return null;
			}
			return this.GetData(format.FullName);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000A9AE4 File Offset: 0x000A7CE4
		public virtual bool GetDataPresent(Type format)
		{
			return !(format == null) && this.GetDataPresent(format.FullName);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000A9B0C File Offset: 0x000A7D0C
		public virtual bool GetDataPresent(string format, bool autoConvert)
		{
			return this.innerData.GetDataPresent(format, autoConvert);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000A9B28 File Offset: 0x000A7D28
		public virtual bool GetDataPresent(string format)
		{
			return this.GetDataPresent(format, true);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000A9B3F File Offset: 0x000A7D3F
		public virtual string[] GetFormats(bool autoConvert)
		{
			return this.innerData.GetFormats(autoConvert);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000A9B4D File Offset: 0x000A7D4D
		public virtual string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000A9B56 File Offset: 0x000A7D56
		public virtual bool ContainsAudio()
		{
			return this.GetDataPresent(DataFormats.WaveAudio, false);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000A9B64 File Offset: 0x000A7D64
		public virtual bool ContainsFileDropList()
		{
			return this.GetDataPresent(DataFormats.FileDrop, true);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000A9B72 File Offset: 0x000A7D72
		public virtual bool ContainsImage()
		{
			return this.GetDataPresent(DataFormats.Bitmap, true);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000A9B80 File Offset: 0x000A7D80
		public virtual bool ContainsText()
		{
			return this.ContainsText(TextDataFormat.UnicodeText);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000A9B89 File Offset: 0x000A7D89
		public virtual bool ContainsText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			return this.GetDataPresent(DataObject.ConvertToDataFormats(format), false);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000A9BBE File Offset: 0x000A7DBE
		public virtual Stream GetAudioStream()
		{
			return this.GetData(DataFormats.WaveAudio, false) as Stream;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A9BD4 File Offset: 0x000A7DD4
		public virtual StringCollection GetFileDropList()
		{
			StringCollection stringCollection = new StringCollection();
			string[] array = this.GetData(DataFormats.FileDrop, true) as string[];
			if (array != null)
			{
				stringCollection.AddRange(array);
			}
			return stringCollection;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A9C04 File Offset: 0x000A7E04
		public virtual Image GetImage()
		{
			return this.GetData(DataFormats.Bitmap, true) as Image;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000A9C17 File Offset: 0x000A7E17
		public virtual string GetText()
		{
			return this.GetText(TextDataFormat.UnicodeText);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000A9C20 File Offset: 0x000A7E20
		public virtual string GetText(TextDataFormat format)
		{
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			string text = this.GetData(DataObject.ConvertToDataFormats(format), false) as string;
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000A9C70 File Offset: 0x000A7E70
		public virtual void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			this.SetAudio(new MemoryStream(audioBytes));
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000A9C8C File Offset: 0x000A7E8C
		public virtual void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			this.SetData(DataFormats.WaveAudio, false, audioStream);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000A9CAC File Offset: 0x000A7EAC
		public virtual void SetFileDropList(StringCollection filePaths)
		{
			if (filePaths == null)
			{
				throw new ArgumentNullException("filePaths");
			}
			string[] array = new string[filePaths.Count];
			filePaths.CopyTo(array, 0);
			this.SetData(DataFormats.FileDrop, true, array);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000A9CE8 File Offset: 0x000A7EE8
		public virtual void SetImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.SetData(DataFormats.Bitmap, true, image);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000A9D05 File Offset: 0x000A7F05
		public virtual void SetText(string textData)
		{
			this.SetText(textData, TextDataFormat.UnicodeText);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000A9D10 File Offset: 0x000A7F10
		public virtual void SetText(string textData, TextDataFormat format)
		{
			if (string.IsNullOrEmpty(textData))
			{
				throw new ArgumentNullException("textData");
			}
			if (!ClientUtils.IsEnumValid(format, (int)format, 0, 4))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			this.SetData(DataObject.ConvertToDataFormats(format), false, textData);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000A9D64 File Offset: 0x000A7F64
		private static string ConvertToDataFormats(TextDataFormat format)
		{
			switch (format)
			{
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

		// Token: 0x060023A3 RID: 9123 RVA: 0x000A9DA0 File Offset: 0x000A7FA0
		private static string[] GetDistinctStrings(string[] formats)
		{
			ArrayList arrayList = new ArrayList();
			foreach (string text in formats)
			{
				if (!arrayList.Contains(text))
				{
					arrayList.Add(text);
				}
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000A9DEC File Offset: 0x000A7FEC
		private static string[] GetMappedFormats(string format)
		{
			if (format == null)
			{
				return null;
			}
			if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.UnicodeText) || format.Equals(DataFormats.StringFormat))
			{
				return new string[]
				{
					DataFormats.StringFormat,
					DataFormats.UnicodeText,
					DataFormats.Text
				};
			}
			if (format.Equals(DataFormats.FileDrop) || format.Equals(DataObject.CF_DEPRECATED_FILENAME) || format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
			{
				return new string[]
				{
					DataFormats.FileDrop,
					DataObject.CF_DEPRECATED_FILENAMEW,
					DataObject.CF_DEPRECATED_FILENAME
				};
			}
			if (format.Equals(DataFormats.Bitmap) || format.Equals(typeof(Bitmap).FullName))
			{
				return new string[]
				{
					typeof(Bitmap).FullName,
					DataFormats.Bitmap
				};
			}
			return new string[]
			{
				format
			};
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000A9EDC File Offset: 0x000A80DC
		private bool GetTymedUseable(TYMED tymed)
		{
			for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
			{
				if ((tymed & DataObject.ALLOWED_TYMEDS[i]) != TYMED.TYMED_NULL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000A9F0C File Offset: 0x000A810C
		private void GetDataIntoOleStructs(ref FORMATETC formatetc, ref STGMEDIUM medium)
		{
			if (this.GetTymedUseable(formatetc.tymed) && this.GetTymedUseable(medium.tymed))
			{
				string name = DataFormats.GetFormat((int)formatetc.cfFormat).Name;
				if (!this.GetDataPresent(name))
				{
					Marshal.ThrowExceptionForHR(-2147221404);
					return;
				}
				object data = this.GetData(name);
				if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
				{
					int num = this.SaveDataToHandle(data, name, ref medium);
					if (NativeMethods.Failed(num))
					{
						Marshal.ThrowExceptionForHR(num);
						return;
					}
				}
				else
				{
					if ((formatetc.tymed & TYMED.TYMED_GDI) == TYMED.TYMED_NULL)
					{
						Marshal.ThrowExceptionForHR(-2147221399);
						return;
					}
					if (name.Equals(DataFormats.Bitmap) && data is Bitmap)
					{
						Bitmap bitmap = (Bitmap)data;
						if (bitmap != null)
						{
							medium.unionmember = this.GetCompatibleBitmap(bitmap);
							return;
						}
					}
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(-2147221399);
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000A9FD8 File Offset: 0x000A81D8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink pAdvSink, out int pdwConnection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.DAdvise(ref pFormatetc, advf, pAdvSink, out pdwConnection);
			}
			pdwConnection = 0;
			return -2147467263;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000AA00B File Offset: 0x000A820B
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.DUnadvise(int dwConnection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.DUnadvise(dwConnection);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147467263);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000AA03B File Offset: 0x000A823B
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.EnumDAdvise(out enumAdvise);
			}
			enumAdvise = null;
			return -2147221501;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000AA06C File Offset: 0x000A826C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		IEnumFORMATETC IDataObject.EnumFormatEtc(DATADIR dwDirection)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.EnumFormatEtc(dwDirection);
			}
			if (dwDirection == DATADIR.DATADIR_GET)
			{
				return new DataObject.FormatEnumerator(this);
			}
			throw new ExternalException(SR.GetString("ExternalException"), -2147467263);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000AA0BC File Offset: 0x000A82BC
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.GetCanonicalFormatEtc(ref FORMATETC pformatetcIn, out FORMATETC pformatetcOut)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.GetCanonicalFormatEtc(ref pformatetcIn, out pformatetcOut);
			}
			pformatetcOut = default(FORMATETC);
			return 262448;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000AA0F0 File Offset: 0x000A82F0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.GetData(ref FORMATETC formatetc, out STGMEDIUM medium)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.GetData(ref formatetc, out medium);
				return;
			}
			medium = default(STGMEDIUM);
			if (this.GetTymedUseable(formatetc.tymed))
			{
				if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
				{
					medium.tymed = TYMED.TYMED_HGLOBAL;
					medium.unionmember = UnsafeNativeMethods.GlobalAlloc(8258, 1);
					if (medium.unionmember == IntPtr.Zero)
					{
						throw new OutOfMemoryException();
					}
					try
					{
						((IDataObject)this).GetDataHere(ref formatetc, ref medium);
						return;
					}
					catch
					{
						UnsafeNativeMethods.GlobalFree(new HandleRef(medium, medium.unionmember));
						medium.unionmember = IntPtr.Zero;
						throw;
					}
				}
				medium.tymed = formatetc.tymed;
				((IDataObject)this).GetDataHere(ref formatetc, ref medium);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147221399);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000AA1D8 File Offset: 0x000A83D8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.GetDataHere(ref FORMATETC formatetc, ref STGMEDIUM medium)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.GetDataHere(ref formatetc, ref medium);
				return;
			}
			this.GetDataIntoOleStructs(ref formatetc, ref medium);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000AA208 File Offset: 0x000A8408
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int IDataObject.QueryGetData(ref FORMATETC formatetc)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this.innerData).OleDataObject.QueryGetData(ref formatetc);
			}
			if (formatetc.dwAspect != DVASPECT.DVASPECT_CONTENT)
			{
				return -2147221397;
			}
			if (!this.GetTymedUseable(formatetc.tymed))
			{
				return -2147221399;
			}
			if (formatetc.cfFormat == 0)
			{
				return 1;
			}
			if (!this.GetDataPresent(DataFormats.GetFormat((int)formatetc.cfFormat).Name))
			{
				return -2147221404;
			}
			return 0;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000AA285 File Offset: 0x000A8485
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void IDataObject.SetData(ref FORMATETC pFormatetcIn, ref STGMEDIUM pmedium, bool fRelease)
		{
			if (this.innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this.innerData).OleDataObject.SetData(ref pFormatetcIn, ref pmedium, fRelease);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000AA2B4 File Offset: 0x000A84B4
		private int SaveDataToHandle(object data, string format, ref STGMEDIUM medium)
		{
			int result = -2147467259;
			if (data is Stream)
			{
				result = this.SaveStreamToHandle(ref medium.unionmember, (Stream)data);
			}
			else if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.Rtf) || format.Equals(DataFormats.OemText))
			{
				result = this.SaveStringToHandle(medium.unionmember, data.ToString(), false);
			}
			else if (format.Equals(DataFormats.Html))
			{
				if (WindowsFormsUtils.TargetsAtLeast_v4_5)
				{
					result = this.SaveHtmlToHandle(medium.unionmember, data.ToString());
				}
				else
				{
					result = this.SaveStringToHandle(medium.unionmember, data.ToString(), false);
				}
			}
			else if (format.Equals(DataFormats.UnicodeText))
			{
				result = this.SaveStringToHandle(medium.unionmember, data.ToString(), true);
			}
			else if (format.Equals(DataFormats.FileDrop))
			{
				result = this.SaveFileListToHandle(medium.unionmember, (string[])data);
			}
			else if (format.Equals(DataObject.CF_DEPRECATED_FILENAME))
			{
				string[] array = (string[])data;
				result = this.SaveStringToHandle(medium.unionmember, array[0], false);
			}
			else if (format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
			{
				string[] array2 = (string[])data;
				result = this.SaveStringToHandle(medium.unionmember, array2[0], true);
			}
			else if (format.Equals(DataFormats.Dib) && data is Image)
			{
				result = -2147221399;
			}
			else if (format.Equals(DataFormats.Serializable) || data is ISerializable || (data != null && data.GetType().IsSerializable))
			{
				result = this.SaveObjectToHandle(ref medium.unionmember, data);
			}
			return result;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000AA458 File Offset: 0x000A8658
		private int SaveObjectToHandle(ref IntPtr handle, object data)
		{
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(DataObject.serializedObjectID);
			DataObject.SaveObjectToHandleSerializer(stream, data);
			return this.SaveStreamToHandle(ref handle, stream);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000AA48C File Offset: 0x000A868C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static void SaveObjectToHandleSerializer(Stream stream, object data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, data);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000AA4A8 File Offset: 0x000A86A8
		private int SaveStreamToHandle(ref IntPtr handle, Stream stream)
		{
			if (handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(null, handle));
			}
			int num = (int)stream.Length;
			handle = UnsafeNativeMethods.GlobalAlloc(8194, num);
			if (handle == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
			if (intPtr == IntPtr.Zero)
			{
				return -2147024882;
			}
			try
			{
				byte[] array = new byte[num];
				stream.Position = 0L;
				stream.Read(array, 0, num);
				Marshal.Copy(array, 0, intPtr, num);
			}
			finally
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
			}
			return 0;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000AA560 File Offset: 0x000A8760
		private int SaveFileListToHandle(IntPtr handle, string[] files)
		{
			if (files == null)
			{
				return 0;
			}
			if (files.Length < 1)
			{
				return 0;
			}
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			bool flag = Marshal.SystemDefaultCharSize != 1;
			IntPtr intPtr = IntPtr.Zero;
			int num = 20;
			int num2 = num;
			if (flag)
			{
				for (int i = 0; i < files.Length; i++)
				{
					num2 += (files[i].Length + 1) * 2;
				}
				num2 += 2;
			}
			else
			{
				for (int j = 0; j < files.Length; j++)
				{
					num2 += NativeMethods.Util.GetPInvokeStringLength(files[j]) + 1;
				}
				num2++;
			}
			IntPtr intPtr2 = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), num2, 8194);
			if (intPtr2 == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr3 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr2));
			if (intPtr3 == IntPtr.Zero)
			{
				return -2147024882;
			}
			intPtr = intPtr3;
			int[] array = new int[5];
			array[0] = num;
			int[] array2 = array;
			if (flag)
			{
				array2[4] = -1;
			}
			Marshal.Copy(array2, 0, intPtr, array2.Length);
			intPtr = (IntPtr)((long)intPtr + (long)num);
			for (int k = 0; k < files.Length; k++)
			{
				if (flag)
				{
					UnsafeNativeMethods.CopyMemoryW(intPtr, files[k], files[k].Length * 2);
					intPtr = (IntPtr)((long)intPtr + (long)(files[k].Length * 2));
					Marshal.Copy(new byte[2], 0, intPtr, 2);
					intPtr = (IntPtr)((long)intPtr + 2L);
				}
				else
				{
					int pinvokeStringLength = NativeMethods.Util.GetPInvokeStringLength(files[k]);
					UnsafeNativeMethods.CopyMemoryA(intPtr, files[k], pinvokeStringLength);
					intPtr = (IntPtr)((long)intPtr + (long)pinvokeStringLength);
					Marshal.Copy(new byte[1], 0, intPtr, 1);
					intPtr = (IntPtr)((long)intPtr + 1L);
				}
			}
			if (flag)
			{
				Marshal.Copy(new char[1], 0, intPtr, 1);
				intPtr = (IntPtr)((long)intPtr + 2L);
			}
			else
			{
				Marshal.Copy(new byte[1], 0, intPtr, 1);
				intPtr = (IntPtr)((long)intPtr + 1L);
			}
			UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr2));
			return 0;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000AA76C File Offset: 0x000A896C
		private int SaveStringToHandle(IntPtr handle, string str, bool unicode)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			IntPtr intPtr = IntPtr.Zero;
			if (unicode)
			{
				int bytes = str.Length * 2 + 2;
				intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), bytes, 8258);
				if (intPtr == IntPtr.Zero)
				{
					return -2147024882;
				}
				IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
				if (intPtr2 == IntPtr.Zero)
				{
					return -2147024882;
				}
				char[] array = str.ToCharArray(0, str.Length);
				UnsafeNativeMethods.CopyMemoryW(intPtr2, array, array.Length * 2);
			}
			else
			{
				int num = UnsafeNativeMethods.WideCharToMultiByte(0, 0, str, str.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
				byte[] array2 = new byte[num];
				UnsafeNativeMethods.WideCharToMultiByte(0, 0, str, str.Length, array2, array2.Length, IntPtr.Zero, IntPtr.Zero);
				intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), num + 1, 8258);
				if (intPtr == IntPtr.Zero)
				{
					return -2147024882;
				}
				IntPtr intPtr3 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
				if (intPtr3 == IntPtr.Zero)
				{
					return -2147024882;
				}
				UnsafeNativeMethods.CopyMemory(intPtr3, array2, num);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr3 + (long)num), 1);
			}
			if (intPtr != IntPtr.Zero)
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			}
			return 0;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
		private int SaveHtmlToHandle(IntPtr handle, string str)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			IntPtr intPtr = IntPtr.Zero;
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] bytes = utf8Encoding.GetBytes(str);
			intPtr = UnsafeNativeMethods.GlobalReAlloc(new HandleRef(null, handle), bytes.Length + 1, 8258);
			if (intPtr == IntPtr.Zero)
			{
				return -2147024882;
			}
			IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
			if (intPtr2 == IntPtr.Zero)
			{
				return -2147024882;
			}
			try
			{
				UnsafeNativeMethods.CopyMemory(intPtr2, bytes, bytes.Length);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr2 + (long)bytes.Length), 1);
			}
			finally
			{
				UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			}
			return 0;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000AA9A0 File Offset: 0x000A8BA0
		public virtual void SetData(string format, bool autoConvert, object data)
		{
			this.innerData.SetData(format, autoConvert, data);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000AA9B0 File Offset: 0x000A8BB0
		public virtual void SetData(string format, object data)
		{
			this.innerData.SetData(format, data);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000AA9BF File Offset: 0x000A8BBF
		public virtual void SetData(Type format, object data)
		{
			this.innerData.SetData(format, data);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000AA9CE File Offset: 0x000A8BCE
		public virtual void SetData(object data)
		{
			this.innerData.SetData(data);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000AA9DC File Offset: 0x000A8BDC
		// Note: this type is marked as 'beforefieldinit'.
		static DataObject()
		{
			TYMED[] array = new TYMED[5];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.962C732E4EFE979C16B0229A7A7F6E81EC12A8FD55D25E4E7567E185D4A70D20).FieldHandle);
			DataObject.ALLOWED_TYMEDS = array;
			DataObject.serializedObjectID = new Guid("FD9EA796-3B13-4370-A679-56106BB288FB").ToByteArray();
		}

		// Token: 0x04000EA6 RID: 3750
		private static readonly string CF_DEPRECATED_FILENAME = "FileName";

		// Token: 0x04000EA7 RID: 3751
		private static readonly string CF_DEPRECATED_FILENAMEW = "FileNameW";

		// Token: 0x04000EA8 RID: 3752
		private const int DV_E_FORMATETC = -2147221404;

		// Token: 0x04000EA9 RID: 3753
		private const int DV_E_LINDEX = -2147221400;

		// Token: 0x04000EAA RID: 3754
		private const int DV_E_TYMED = -2147221399;

		// Token: 0x04000EAB RID: 3755
		private const int DV_E_DVASPECT = -2147221397;

		// Token: 0x04000EAC RID: 3756
		private const int OLE_E_NOTRUNNING = -2147221499;

		// Token: 0x04000EAD RID: 3757
		private const int OLE_E_ADVISENOTSUPPORTED = -2147221501;

		// Token: 0x04000EAE RID: 3758
		private const int DATA_S_SAMEFORMATETC = 262448;

		// Token: 0x04000EAF RID: 3759
		private static readonly TYMED[] ALLOWED_TYMEDS;

		// Token: 0x04000EB0 RID: 3760
		private IDataObject innerData;

		// Token: 0x04000EB2 RID: 3762
		private static readonly byte[] serializedObjectID;

		// Token: 0x0200067F RID: 1663
		private class FormatEnumerator : IEnumFORMATETC
		{
			// Token: 0x060066E4 RID: 26340 RVA: 0x00180F44 File Offset: 0x0017F144
			public FormatEnumerator(IDataObject parent) : this(parent, parent.GetFormats())
			{
			}

			// Token: 0x060066E5 RID: 26341 RVA: 0x00180F54 File Offset: 0x0017F154
			public FormatEnumerator(IDataObject parent, FORMATETC[] formats)
			{
				this.formats = new ArrayList();
				base..ctor();
				this.formats.Clear();
				this.parent = parent;
				this.current = 0;
				if (formats != null)
				{
					DataObject dataObject = parent as DataObject;
					if (dataObject != null && dataObject.RestrictedFormats && !Clipboard.IsFormatValid(formats))
					{
						throw new SecurityException(SR.GetString("ClipboardSecurityException"));
					}
					foreach (FORMATETC formatetc in formats)
					{
						FORMATETC formatetc2 = default(FORMATETC);
						formatetc2.cfFormat = formatetc.cfFormat;
						formatetc2.dwAspect = formatetc.dwAspect;
						formatetc2.ptd = formatetc.ptd;
						formatetc2.lindex = formatetc.lindex;
						formatetc2.tymed = formatetc.tymed;
						this.formats.Add(formatetc2);
					}
				}
			}

			// Token: 0x060066E6 RID: 26342 RVA: 0x0018102C File Offset: 0x0017F22C
			public FormatEnumerator(IDataObject parent, string[] formats)
			{
				this.formats = new ArrayList();
				base..ctor();
				this.parent = parent;
				this.formats.Clear();
				string bitmap = DataFormats.Bitmap;
				string enhancedMetafile = DataFormats.EnhancedMetafile;
				string text = DataFormats.Text;
				string unicodeText = DataFormats.UnicodeText;
				string stringFormat = DataFormats.StringFormat;
				string stringFormat2 = DataFormats.StringFormat;
				if (formats != null)
				{
					DataObject dataObject = parent as DataObject;
					if (dataObject != null && dataObject.RestrictedFormats && !Clipboard.IsFormatValid(formats))
					{
						throw new SecurityException(SR.GetString("ClipboardSecurityException"));
					}
					foreach (string text2 in formats)
					{
						FORMATETC formatetc = default(FORMATETC);
						formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(text2).Id);
						formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
						formatetc.ptd = IntPtr.Zero;
						formatetc.lindex = -1;
						if (text2.Equals(bitmap))
						{
							formatetc.tymed = TYMED.TYMED_GDI;
						}
						else if (text2.Equals(enhancedMetafile))
						{
							formatetc.tymed = TYMED.TYMED_ENHMF;
						}
						else if (text2.Equals(text) || text2.Equals(unicodeText) || text2.Equals(stringFormat) || text2.Equals(stringFormat2) || text2.Equals(DataFormats.Rtf) || text2.Equals(DataFormats.CommaSeparatedValue) || text2.Equals(DataFormats.FileDrop) || text2.Equals(DataObject.CF_DEPRECATED_FILENAME) || text2.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
						{
							formatetc.tymed = TYMED.TYMED_HGLOBAL;
						}
						else
						{
							formatetc.tymed = TYMED.TYMED_HGLOBAL;
						}
						if (formatetc.tymed != TYMED.TYMED_NULL)
						{
							this.formats.Add(formatetc);
						}
					}
				}
			}

			// Token: 0x060066E7 RID: 26343 RVA: 0x001811DC File Offset: 0x0017F3DC
			public int Next(int celt, FORMATETC[] rgelt, int[] pceltFetched)
			{
				if (this.current < this.formats.Count && celt > 0)
				{
					FORMATETC formatetc = (FORMATETC)this.formats[this.current];
					rgelt[0].cfFormat = formatetc.cfFormat;
					rgelt[0].tymed = formatetc.tymed;
					rgelt[0].dwAspect = DVASPECT.DVASPECT_CONTENT;
					rgelt[0].ptd = IntPtr.Zero;
					rgelt[0].lindex = -1;
					if (pceltFetched != null)
					{
						pceltFetched[0] = 1;
					}
					this.current++;
					return 0;
				}
				if (pceltFetched != null)
				{
					pceltFetched[0] = 0;
				}
				return 1;
			}

			// Token: 0x060066E8 RID: 26344 RVA: 0x0018128A File Offset: 0x0017F48A
			public int Skip(int celt)
			{
				if (this.current + celt >= this.formats.Count)
				{
					return 1;
				}
				this.current += celt;
				return 0;
			}

			// Token: 0x060066E9 RID: 26345 RVA: 0x001812B2 File Offset: 0x0017F4B2
			public int Reset()
			{
				this.current = 0;
				return 0;
			}

			// Token: 0x060066EA RID: 26346 RVA: 0x001812BC File Offset: 0x0017F4BC
			public void Clone(out IEnumFORMATETC ppenum)
			{
				FORMATETC[] array = new FORMATETC[this.formats.Count];
				this.formats.CopyTo(array, 0);
				ppenum = new DataObject.FormatEnumerator(this.parent, array);
			}

			// Token: 0x04003A86 RID: 14982
			internal IDataObject parent;

			// Token: 0x04003A87 RID: 14983
			internal ArrayList formats;

			// Token: 0x04003A88 RID: 14984
			internal int current;
		}

		// Token: 0x02000680 RID: 1664
		private class OleConverter : IDataObject
		{
			// Token: 0x060066EB RID: 26347 RVA: 0x001812F5 File Offset: 0x0017F4F5
			public OleConverter(IDataObject data)
			{
				this.innerData = data;
			}

			// Token: 0x1700166C RID: 5740
			// (get) Token: 0x060066EC RID: 26348 RVA: 0x00181304 File Offset: 0x0017F504
			public IDataObject OleDataObject
			{
				get
				{
					return this.innerData;
				}
			}

			// Token: 0x060066ED RID: 26349 RVA: 0x0018130C File Offset: 0x0017F50C
			private object GetDataFromOleIStream(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = TYMED.TYMED_ISTREAM;
				stgmedium.tymed = TYMED.TYMED_ISTREAM;
				if (this.QueryGetDataUnsafe(ref formatetc) != 0)
				{
					return null;
				}
				try
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						this.innerData.GetData(ref formatetc, out stgmedium);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				catch
				{
					return null;
				}
				if (stgmedium.unionmember != IntPtr.Zero)
				{
					UnsafeNativeMethods.IStream stream = (UnsafeNativeMethods.IStream)Marshal.GetObjectForIUnknown(stgmedium.unionmember);
					Marshal.Release(stgmedium.unionmember);
					NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
					stream.Stat(statstg, 0);
					int num = (int)statstg.cbSize;
					IntPtr intPtr = UnsafeNativeMethods.GlobalAlloc(8258, num);
					IntPtr buf = UnsafeNativeMethods.GlobalLock(new HandleRef(this.innerData, intPtr));
					stream.Read(buf, num);
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(this.innerData, intPtr));
					return this.GetDataFromHGLOBLAL(format, intPtr);
				}
				return null;
			}

			// Token: 0x060066EE RID: 26350 RVA: 0x0018144C File Offset: 0x0017F64C
			private object GetDataFromHGLOBLAL(string format, IntPtr hglobal)
			{
				object result = null;
				if (hglobal != IntPtr.Zero)
				{
					if (format.Equals(DataFormats.Text) || format.Equals(DataFormats.Rtf) || format.Equals(DataFormats.OemText))
					{
						result = this.ReadStringFromHandle(hglobal, false);
					}
					else if (format.Equals(DataFormats.Html))
					{
						if (WindowsFormsUtils.TargetsAtLeast_v4_5)
						{
							result = this.ReadHtmlFromHandle(hglobal);
						}
						else
						{
							result = this.ReadStringFromHandle(hglobal, false);
						}
					}
					else if (format.Equals(DataFormats.UnicodeText))
					{
						result = this.ReadStringFromHandle(hglobal, true);
					}
					else if (format.Equals(DataFormats.FileDrop))
					{
						result = this.ReadFileListFromHandle(hglobal);
					}
					else if (format.Equals(DataObject.CF_DEPRECATED_FILENAME))
					{
						result = new string[]
						{
							this.ReadStringFromHandle(hglobal, false)
						};
					}
					else if (format.Equals(DataObject.CF_DEPRECATED_FILENAMEW))
					{
						result = new string[]
						{
							this.ReadStringFromHandle(hglobal, true)
						};
					}
					else if (!LocalAppContextSwitches.EnableLegacyDangerousClipboardDeserializationMode)
					{
						bool restrictDeserialization = format.Equals(DataFormats.StringFormat) || format.Equals(typeof(Bitmap).FullName) || format.Equals(DataFormats.CommaSeparatedValue) || format.Equals(DataFormats.Dib) || format.Equals(DataFormats.Dif) || format.Equals(DataFormats.Locale) || format.Equals(DataFormats.PenData) || format.Equals(DataFormats.Riff) || format.Equals(DataFormats.SymbolicLink) || format.Equals(DataFormats.Tiff) || format.Equals(DataFormats.WaveAudio) || format.Equals(DataFormats.Bitmap) || format.Equals(DataFormats.EnhancedMetafile) || format.Equals(DataFormats.Palette) || format.Equals(DataFormats.MetafilePict);
						result = this.ReadObjectFromHandle(hglobal, restrictDeserialization);
					}
					else
					{
						result = this.ReadObjectFromHandle(hglobal, false);
					}
					UnsafeNativeMethods.GlobalFree(new HandleRef(null, hglobal));
				}
				return result;
			}

			// Token: 0x060066EF RID: 26351 RVA: 0x00181658 File Offset: 0x0017F858
			private object GetDataFromOleHGLOBAL(string format, out bool done)
			{
				done = false;
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = TYMED.TYMED_HGLOBAL;
				stgmedium.tymed = TYMED.TYMED_HGLOBAL;
				object result = null;
				if (this.QueryGetDataUnsafe(ref formatetc) == 0)
				{
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							this.innerData.GetData(ref formatetc, out stgmedium);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (stgmedium.unionmember != IntPtr.Zero)
						{
							result = this.GetDataFromHGLOBLAL(format, stgmedium.unionmember);
						}
					}
					catch (DataObject.OleConverter.RestrictedTypeDeserializationException)
					{
						done = true;
					}
					catch
					{
					}
				}
				return result;
			}

			// Token: 0x060066F0 RID: 26352 RVA: 0x00181730 File Offset: 0x0017F930
			private object GetDataFromOleOther(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				STGMEDIUM stgmedium = default(STGMEDIUM);
				TYMED tymed = TYMED.TYMED_NULL;
				if (format.Equals(DataFormats.Bitmap))
				{
					tymed = TYMED.TYMED_GDI;
				}
				else if (format.Equals(DataFormats.EnhancedMetafile))
				{
					tymed = TYMED.TYMED_ENHMF;
				}
				if (tymed == TYMED.TYMED_NULL)
				{
					return null;
				}
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				formatetc.tymed = tymed;
				stgmedium.tymed = tymed;
				object result = null;
				if (this.QueryGetDataUnsafe(ref formatetc) == 0)
				{
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							this.innerData.GetData(ref formatetc, out stgmedium);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch
					{
					}
				}
				if (stgmedium.unionmember != IntPtr.Zero && format.Equals(DataFormats.Bitmap))
				{
					System.Internal.HandleCollector.Add(stgmedium.unionmember, NativeMethods.CommonHandles.GDI);
					Image image = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						image = Image.FromHbitmap(stgmedium.unionmember);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (image != null)
					{
						Image image2 = image;
						image = (Image)image.Clone();
						SafeNativeMethods.DeleteObject(new HandleRef(null, stgmedium.unionmember));
						image2.Dispose();
					}
					result = image;
				}
				return result;
			}

			// Token: 0x060066F1 RID: 26353 RVA: 0x0018188C File Offset: 0x0017FA8C
			private object GetDataFromBoundOleDataObject(string format, out bool done)
			{
				object obj = null;
				done = false;
				try
				{
					obj = this.GetDataFromOleOther(format);
					if (obj == null)
					{
						obj = this.GetDataFromOleHGLOBAL(format, out done);
					}
					if (obj == null && !done)
					{
						obj = this.GetDataFromOleIStream(format);
					}
				}
				catch (Exception ex)
				{
				}
				return obj;
			}

			// Token: 0x060066F2 RID: 26354 RVA: 0x001818D8 File Offset: 0x0017FAD8
			private Stream ReadByteStreamFromHandle(IntPtr handle, out bool isSerializedObject)
			{
				IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					throw new ExternalException(SR.GetString("ExternalException"), -2147024882);
				}
				Stream result;
				try
				{
					int num = UnsafeNativeMethods.GlobalSize(new HandleRef(null, handle));
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					int num2 = 0;
					if (num > DataObject.serializedObjectID.Length)
					{
						isSerializedObject = true;
						for (int i = 0; i < DataObject.serializedObjectID.Length; i++)
						{
							if (DataObject.serializedObjectID[i] != array[i])
							{
								isSerializedObject = false;
								break;
							}
						}
						if (isSerializedObject)
						{
							num2 = DataObject.serializedObjectID.Length;
						}
					}
					else
					{
						isSerializedObject = false;
					}
					result = new MemoryStream(array, num2, array.Length - num2);
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return result;
			}

			// Token: 0x060066F3 RID: 26355 RVA: 0x001819AC File Offset: 0x0017FBAC
			private object ReadObjectFromHandle(IntPtr handle, bool restrictDeserialization)
			{
				bool flag;
				Stream stream = this.ReadByteStreamFromHandle(handle, out flag);
				object result;
				if (flag)
				{
					result = DataObject.OleConverter.ReadObjectFromHandleDeserializer(stream, restrictDeserialization);
				}
				else
				{
					result = stream;
				}
				return result;
			}

			// Token: 0x060066F4 RID: 26356 RVA: 0x001819D8 File Offset: 0x0017FBD8
			private static object ReadObjectFromHandleDeserializer(Stream stream, bool restrictDeserialization)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (restrictDeserialization)
				{
					binaryFormatter.Binder = new DataObject.OleConverter.RestrictiveBinder();
				}
				binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				return binaryFormatter.Deserialize(stream);
			}

			// Token: 0x060066F5 RID: 26357 RVA: 0x00181A08 File Offset: 0x0017FC08
			private string[] ReadFileListFromHandle(IntPtr hdrop)
			{
				string[] array = null;
				StringBuilder stringBuilder = new StringBuilder(260);
				int num = UnsafeNativeMethods.DragQueryFile(new HandleRef(null, hdrop), -1, null, 0);
				if (num > 0)
				{
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						int num2 = UnsafeNativeMethods.DragQueryFileLongPath(new HandleRef(null, hdrop), i, stringBuilder);
						if (num2 != 0)
						{
							string text = stringBuilder.ToString(0, num2);
							string fullPath = Path.GetFullPath(text);
							new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPath).Demand();
							array[i] = text;
						}
					}
				}
				return array;
			}

			// Token: 0x060066F6 RID: 26358 RVA: 0x00181A84 File Offset: 0x0017FC84
			private unsafe string ReadStringFromHandle(IntPtr handle, bool unicode)
			{
				string result = null;
				IntPtr value = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				try
				{
					if (unicode)
					{
						result = new string((char*)((void*)value));
					}
					else
					{
						result = new string((sbyte*)((void*)value));
					}
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return result;
			}

			// Token: 0x060066F7 RID: 26359 RVA: 0x00181AE0 File Offset: 0x0017FCE0
			private string ReadHtmlFromHandle(IntPtr handle)
			{
				string result = null;
				IntPtr source = UnsafeNativeMethods.GlobalLock(new HandleRef(null, handle));
				try
				{
					int num = UnsafeNativeMethods.GlobalSize(new HandleRef(null, handle));
					byte[] array = new byte[num];
					Marshal.Copy(source, array, 0, num);
					result = Encoding.UTF8.GetString(array);
				}
				finally
				{
					UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, handle));
				}
				return result;
			}

			// Token: 0x060066F8 RID: 26360 RVA: 0x00181B48 File Offset: 0x0017FD48
			public virtual object GetData(string format, bool autoConvert)
			{
				bool flag = false;
				object dataFromBoundOleDataObject = this.GetDataFromBoundOleDataObject(format, out flag);
				object obj = dataFromBoundOleDataObject;
				if (!flag && autoConvert && (dataFromBoundOleDataObject == null || dataFromBoundOleDataObject is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						int num = 0;
						while (!flag && num < mappedFormats.Length)
						{
							if (!format.Equals(mappedFormats[num]))
							{
								dataFromBoundOleDataObject = this.GetDataFromBoundOleDataObject(mappedFormats[num], out flag);
								if (!flag && dataFromBoundOleDataObject != null && !(dataFromBoundOleDataObject is MemoryStream))
								{
									obj = null;
									break;
								}
							}
							num++;
						}
					}
				}
				if (obj != null)
				{
					return obj;
				}
				return dataFromBoundOleDataObject;
			}

			// Token: 0x060066F9 RID: 26361 RVA: 0x00181BC6 File Offset: 0x0017FDC6
			public virtual object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x060066FA RID: 26362 RVA: 0x00181BD0 File Offset: 0x0017FDD0
			public virtual object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x060066FB RID: 26363 RVA: 0x000072B6 File Offset: 0x000054B6
			public virtual void SetData(string format, bool autoConvert, object data)
			{
			}

			// Token: 0x060066FC RID: 26364 RVA: 0x00181BDE File Offset: 0x0017FDDE
			public virtual void SetData(string format, object data)
			{
				this.SetData(format, true, data);
			}

			// Token: 0x060066FD RID: 26365 RVA: 0x00181BE9 File Offset: 0x0017FDE9
			public virtual void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x060066FE RID: 26366 RVA: 0x00181BF8 File Offset: 0x0017FDF8
			public virtual void SetData(object data)
			{
				if (data is ISerializable)
				{
					this.SetData(DataFormats.Serializable, data);
					return;
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x060066FF RID: 26367 RVA: 0x00181C1C File Offset: 0x0017FE1C
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			private int QueryGetDataUnsafe(ref FORMATETC formatetc)
			{
				return this.innerData.QueryGetData(ref formatetc);
			}

			// Token: 0x06006700 RID: 26368 RVA: 0x00181C1C File Offset: 0x0017FE1C
			private int QueryGetDataInner(ref FORMATETC formatetc)
			{
				return this.innerData.QueryGetData(ref formatetc);
			}

			// Token: 0x06006701 RID: 26369 RVA: 0x00181C2A File Offset: 0x0017FE2A
			public virtual bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x06006702 RID: 26370 RVA: 0x00181C38 File Offset: 0x0017FE38
			private bool GetDataPresentInner(string format)
			{
				FORMATETC formatetc = default(FORMATETC);
				formatetc.cfFormat = (short)((ushort)DataFormats.GetFormat(format).Id);
				formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
				formatetc.lindex = -1;
				for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
				{
					formatetc.tymed |= DataObject.ALLOWED_TYMEDS[i];
				}
				int num = this.QueryGetDataUnsafe(ref formatetc);
				return num == 0;
			}

			// Token: 0x06006703 RID: 26371 RVA: 0x00181CA4 File Offset: 0x0017FEA4
			public virtual bool GetDataPresent(string format, bool autoConvert)
			{
				IntSecurity.ClipboardRead.Demand();
				bool flag = false;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					flag = this.GetDataPresentInner(format);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (!flag && autoConvert)
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!format.Equals(mappedFormats[i]))
							{
								IntSecurity.UnmanagedCode.Assert();
								try
								{
									flag = this.GetDataPresentInner(mappedFormats[i]);
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
								if (flag)
								{
									break;
								}
							}
						}
					}
				}
				return flag;
			}

			// Token: 0x06006704 RID: 26372 RVA: 0x00181D40 File Offset: 0x0017FF40
			public virtual bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x06006705 RID: 26373 RVA: 0x00181D4C File Offset: 0x0017FF4C
			public virtual string[] GetFormats(bool autoConvert)
			{
				IEnumFORMATETC enumFORMATETC = null;
				ArrayList arrayList = new ArrayList();
				try
				{
					enumFORMATETC = this.innerData.EnumFormatEtc(DATADIR.DATADIR_GET);
				}
				catch
				{
				}
				if (enumFORMATETC != null)
				{
					enumFORMATETC.Reset();
					FORMATETC[] array = new FORMATETC[1];
					int[] array2 = new int[]
					{
						1
					};
					while (array2[0] > 0)
					{
						array2[0] = 0;
						try
						{
							enumFORMATETC.Next(1, array, array2);
						}
						catch
						{
						}
						if (array2[0] > 0)
						{
							string name = DataFormats.GetFormat((int)array[0].cfFormat).Name;
							if (autoConvert)
							{
								string[] mappedFormats = DataObject.GetMappedFormats(name);
								for (int i = 0; i < mappedFormats.Length; i++)
								{
									arrayList.Add(mappedFormats[i]);
								}
							}
							else
							{
								arrayList.Add(name);
							}
						}
					}
				}
				string[] array3 = new string[arrayList.Count];
				arrayList.CopyTo(array3, 0);
				return DataObject.GetDistinctStrings(array3);
			}

			// Token: 0x06006706 RID: 26374 RVA: 0x00181E3C File Offset: 0x0018003C
			public virtual string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x04003A89 RID: 14985
			internal IDataObject innerData;

			// Token: 0x020008BA RID: 2234
			private class RestrictiveBinder : SerializationBinder
			{
				// Token: 0x060072DB RID: 29403 RVA: 0x001A48E4 File Offset: 0x001A2AE4
				static RestrictiveBinder()
				{
					AssemblyName assemblyName = new AssemblyName(typeof(Bitmap).Assembly.FullName);
					if (assemblyName != null)
					{
						DataObject.OleConverter.RestrictiveBinder.s_allowedAssemblyName = assemblyName.Name;
						DataObject.OleConverter.RestrictiveBinder.s_allowedToken = assemblyName.GetPublicKeyToken();
					}
				}

				// Token: 0x060072DC RID: 29404 RVA: 0x001A4938 File Offset: 0x001A2B38
				public override Type BindToType(string assemblyName, string typeName)
				{
					if (string.CompareOrdinal(typeName, DataObject.OleConverter.RestrictiveBinder.s_allowedTypeName) == 0)
					{
						AssemblyName assemblyName2 = null;
						try
						{
							assemblyName2 = new AssemblyName(assemblyName);
						}
						catch
						{
						}
						if (assemblyName2 != null && string.CompareOrdinal(assemblyName2.Name, DataObject.OleConverter.RestrictiveBinder.s_allowedAssemblyName) == 0)
						{
							byte[] publicKeyToken = assemblyName2.GetPublicKeyToken();
							if (publicKeyToken != null && DataObject.OleConverter.RestrictiveBinder.s_allowedToken != null && publicKeyToken.Length == DataObject.OleConverter.RestrictiveBinder.s_allowedToken.Length)
							{
								bool flag = false;
								for (int i = 0; i < DataObject.OleConverter.RestrictiveBinder.s_allowedToken.Length; i++)
								{
									if (DataObject.OleConverter.RestrictiveBinder.s_allowedToken[i] != publicKeyToken[i])
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									return null;
								}
							}
						}
					}
					throw new DataObject.OleConverter.RestrictedTypeDeserializationException();
				}

				// Token: 0x04004531 RID: 17713
				private static string s_allowedTypeName = typeof(Bitmap).FullName;

				// Token: 0x04004532 RID: 17714
				private static string s_allowedAssemblyName;

				// Token: 0x04004533 RID: 17715
				private static byte[] s_allowedToken;
			}

			// Token: 0x020008BB RID: 2235
			private class RestrictedTypeDeserializationException : Exception
			{
			}
		}

		// Token: 0x02000681 RID: 1665
		private class DataStore : IDataObject
		{
			// Token: 0x06006708 RID: 26376 RVA: 0x00181E60 File Offset: 0x00180060
			public virtual object GetData(string format, bool autoConvert)
			{
				DataObject.DataStore.DataStoreEntry dataStoreEntry = (DataObject.DataStore.DataStoreEntry)this.data[format];
				object obj = null;
				if (dataStoreEntry != null)
				{
					obj = dataStoreEntry.data;
				}
				object obj2 = obj;
				if (autoConvert && (dataStoreEntry == null || dataStoreEntry.autoConvert) && (obj == null || obj is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!format.Equals(mappedFormats[i]))
							{
								DataObject.DataStore.DataStoreEntry dataStoreEntry2 = (DataObject.DataStore.DataStoreEntry)this.data[mappedFormats[i]];
								if (dataStoreEntry2 != null)
								{
									obj = dataStoreEntry2.data;
								}
								if (obj != null && !(obj is MemoryStream))
								{
									obj2 = null;
									break;
								}
							}
						}
					}
				}
				if (obj2 != null)
				{
					return obj2;
				}
				return obj;
			}

			// Token: 0x06006709 RID: 26377 RVA: 0x00181F05 File Offset: 0x00180105
			public virtual object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x0600670A RID: 26378 RVA: 0x00181F0F File Offset: 0x0018010F
			public virtual object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x0600670B RID: 26379 RVA: 0x00181F20 File Offset: 0x00180120
			public virtual void SetData(string format, bool autoConvert, object data)
			{
				if (data is Bitmap && format.Equals(DataFormats.Dib))
				{
					if (!autoConvert)
					{
						throw new NotSupportedException(SR.GetString("DataObjectDibNotSupported"));
					}
					format = DataFormats.Bitmap;
				}
				this.data[format] = new DataObject.DataStore.DataStoreEntry(data, autoConvert);
			}

			// Token: 0x0600670C RID: 26380 RVA: 0x00181F71 File Offset: 0x00180171
			public virtual void SetData(string format, object data)
			{
				this.SetData(format, true, data);
			}

			// Token: 0x0600670D RID: 26381 RVA: 0x00181F7C File Offset: 0x0018017C
			public virtual void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x0600670E RID: 26382 RVA: 0x00181F8B File Offset: 0x0018018B
			public virtual void SetData(object data)
			{
				if (data is ISerializable && !this.data.ContainsKey(DataFormats.Serializable))
				{
					this.SetData(DataFormats.Serializable, data);
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x0600670F RID: 26383 RVA: 0x00181FC0 File Offset: 0x001801C0
			public virtual bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x06006710 RID: 26384 RVA: 0x00181FD0 File Offset: 0x001801D0
			public virtual bool GetDataPresent(string format, bool autoConvert)
			{
				if (!autoConvert)
				{
					return this.data.ContainsKey(format);
				}
				string[] formats = this.GetFormats(autoConvert);
				for (int i = 0; i < formats.Length; i++)
				{
					if (format.Equals(formats[i]))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006711 RID: 26385 RVA: 0x00182011 File Offset: 0x00180211
			public virtual bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x06006712 RID: 26386 RVA: 0x0018201C File Offset: 0x0018021C
			public virtual string[] GetFormats(bool autoConvert)
			{
				string[] array = new string[this.data.Keys.Count];
				this.data.Keys.CopyTo(array, 0);
				if (autoConvert)
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i++)
					{
						if (((DataObject.DataStore.DataStoreEntry)this.data[array[i]]).autoConvert)
						{
							string[] mappedFormats = DataObject.GetMappedFormats(array[i]);
							for (int j = 0; j < mappedFormats.Length; j++)
							{
								arrayList.Add(mappedFormats[j]);
							}
						}
						else
						{
							arrayList.Add(array[i]);
						}
					}
					string[] array2 = new string[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					array = DataObject.GetDistinctStrings(array2);
				}
				return array;
			}

			// Token: 0x06006713 RID: 26387 RVA: 0x001820D3 File Offset: 0x001802D3
			public virtual string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x04003A8A RID: 14986
			private Hashtable data = new Hashtable(BackCompatibleStringComparer.Default);

			// Token: 0x020008BC RID: 2236
			private class DataStoreEntry
			{
				// Token: 0x060072DF RID: 29407 RVA: 0x001A49D8 File Offset: 0x001A2BD8
				public DataStoreEntry(object data, bool autoConvert)
				{
					this.data = data;
					this.autoConvert = autoConvert;
				}

				// Token: 0x04004534 RID: 17716
				public object data;

				// Token: 0x04004535 RID: 17717
				public bool autoConvert;
			}
		}
	}
}
