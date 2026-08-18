using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000079 RID: 121
	public sealed class ZipExtraData : IDisposable
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00017DD3 File Offset: 0x00016DD3
		public ZipExtraData()
		{
			this.Clear();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017DE1 File Offset: 0x00016DE1
		public ZipExtraData(byte[] data)
		{
			if (data == null)
			{
				this._data = new byte[0];
				return;
			}
			this._data = data;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017E00 File Offset: 0x00016E00
		public byte[] GetEntryData()
		{
			if (this.Length > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			return (byte[])this._data.Clone();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017E2A File Offset: 0x00016E2A
		public void Clear()
		{
			if (this._data == null || this._data.Length != 0)
			{
				this._data = new byte[0];
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00017E4A File Offset: 0x00016E4A
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017E54 File Offset: 0x00016E54
		public Stream GetStreamForTag(int tag)
		{
			Stream result = null;
			if (this.Find(tag))
			{
				result = new MemoryStream(this._data, this._index, this._readValueLength, false);
			}
			return result;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017E88 File Offset: 0x00016E88
		public T GetData<T>() where T : class, ITaggedData, new()
		{
			T result = Activator.CreateInstance<T>();
			if (this.Find((int)result.TagID))
			{
				result.SetData(this._data, this._readValueStart, this._readValueLength);
				return result;
			}
			return default(T);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00017EDA File Offset: 0x00016EDA
		public int ValueLength
		{
			get
			{
				return this._readValueLength;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00017EE2 File Offset: 0x00016EE2
		public int CurrentReadIndex
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00017EEA File Offset: 0x00016EEA
		public int UnreadCount
		{
			get
			{
				if (this._readValueStart > this._data.Length || this._readValueStart < 4)
				{
					throw new ZipException("Find must be called before calling a Read method");
				}
				return this._readValueStart + this._readValueLength - this._index;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00017F24 File Offset: 0x00016F24
		public bool Find(int headerID)
		{
			this._readValueStart = this._data.Length;
			this._readValueLength = 0;
			this._index = 0;
			int num = this._readValueStart;
			int num2 = headerID - 1;
			while (num2 != headerID && this._index < this._data.Length - 3)
			{
				num2 = this.ReadShortInternal();
				num = this.ReadShortInternal();
				if (num2 != headerID)
				{
					this._index += num;
				}
			}
			bool flag = num2 == headerID && this._index + num <= this._data.Length;
			if (flag)
			{
				this._readValueStart = this._index;
				this._readValueLength = num;
			}
			return flag;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00017FC4 File Offset: 0x00016FC4
		public void AddEntry(ITaggedData taggedData)
		{
			if (taggedData == null)
			{
				throw new ArgumentNullException("taggedData");
			}
			this.AddEntry((int)taggedData.TagID, taggedData.GetData());
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00017FE8 File Offset: 0x00016FE8
		public void AddEntry(int headerID, byte[] fieldData)
		{
			if (headerID > 65535 || headerID < 0)
			{
				throw new ArgumentOutOfRangeException("headerID");
			}
			int num = (fieldData == null) ? 0 : fieldData.Length;
			if (num > 65535)
			{
				throw new ArgumentOutOfRangeException("fieldData", "exceeds maximum length");
			}
			int num2 = this._data.Length + num + 4;
			if (this.Find(headerID))
			{
				num2 -= this.ValueLength + 4;
			}
			if (num2 > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			this.Delete(headerID);
			byte[] array = new byte[num2];
			this._data.CopyTo(array, 0);
			int index = this._data.Length;
			this._data = array;
			this.SetShort(ref index, headerID);
			this.SetShort(ref index, num);
			if (fieldData != null)
			{
				fieldData.CopyTo(array, index);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000180AB File Offset: 0x000170AB
		public void StartNewEntry()
		{
			this._newEntry = new MemoryStream();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000180B8 File Offset: 0x000170B8
		public void AddNewEntry(int headerID)
		{
			byte[] fieldData = this._newEntry.ToArray();
			this._newEntry = null;
			this.AddEntry(headerID, fieldData);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000180E0 File Offset: 0x000170E0
		public void AddData(byte data)
		{
			this._newEntry.WriteByte(data);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000180EE File Offset: 0x000170EE
		public void AddData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._newEntry.Write(data, 0, data.Length);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001810E File Offset: 0x0001710E
		public void AddLeShort(int toAdd)
		{
			this._newEntry.WriteByte((byte)toAdd);
			this._newEntry.WriteByte((byte)(toAdd >> 8));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001812C File Offset: 0x0001712C
		public void AddLeInt(int toAdd)
		{
			this.AddLeShort((int)((short)toAdd));
			this.AddLeShort((int)((short)(toAdd >> 16)));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00018141 File Offset: 0x00017141
		public void AddLeLong(long toAdd)
		{
			this.AddLeInt((int)(toAdd & (long)((ulong)-1)));
			this.AddLeInt((int)(toAdd >> 32));
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001815C File Offset: 0x0001715C
		public bool Delete(int headerID)
		{
			bool result = false;
			if (this.Find(headerID))
			{
				result = true;
				int num = this._readValueStart - 4;
				byte[] array = new byte[this._data.Length - (this.ValueLength + 4)];
				Array.Copy(this._data, 0, array, 0, num);
				int num2 = num + this.ValueLength + 4;
				Array.Copy(this._data, num2, array, num, this._data.Length - num2);
				this._data = array;
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000181D0 File Offset: 0x000171D0
		public long ReadLong()
		{
			this.ReadCheck(8);
			return ((long)this.ReadInt() & (long)((ulong)-1)) | (long)this.ReadInt() << 32;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000181F0 File Offset: 0x000171F0
		public int ReadInt()
		{
			this.ReadCheck(4);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8) + ((int)this._data[this._index + 2] << 16) + ((int)this._data[this._index + 3] << 24);
			this._index += 4;
			return result;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001825C File Offset: 0x0001725C
		public int ReadShort()
		{
			this.ReadCheck(2);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return result;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000182A0 File Offset: 0x000172A0
		public int ReadByte()
		{
			int result = -1;
			if (this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index)
			{
				result = (int)this._data[this._index];
				this._index++;
			}
			return result;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000182F1 File Offset: 0x000172F1
		public void Skip(int amount)
		{
			this.ReadCheck(amount);
			this._index += amount;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00018308 File Offset: 0x00017308
		private void ReadCheck(int length)
		{
			if (this._readValueStart > this._data.Length || this._readValueStart < 4)
			{
				throw new ZipException("Find must be called before calling a Read method");
			}
			if (this._index > this._readValueStart + this._readValueLength - length)
			{
				throw new ZipException("End of extra data");
			}
			if (this._index + length < 4)
			{
				throw new ZipException("Cannot read before start of tag");
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00018374 File Offset: 0x00017374
		private int ReadShortInternal()
		{
			if (this._index > this._data.Length - 2)
			{
				throw new ZipException("End of extra data");
			}
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return result;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000183CD File Offset: 0x000173CD
		private void SetShort(ref int index, int source)
		{
			this._data[index] = (byte)source;
			this._data[index + 1] = (byte)(source >> 8);
			index += 2;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000183EF File Offset: 0x000173EF
		public void Dispose()
		{
			if (this._newEntry != null)
			{
				this._newEntry.Close();
			}
		}

		// Token: 0x040002F8 RID: 760
		private int _index;

		// Token: 0x040002F9 RID: 761
		private int _readValueStart;

		// Token: 0x040002FA RID: 762
		private int _readValueLength;

		// Token: 0x040002FB RID: 763
		private MemoryStream _newEntry;

		// Token: 0x040002FC RID: 764
		private byte[] _data;
	}
}
