using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000130 RID: 304
	public sealed class SessionStateItemCollection : NameObjectCollectionBase, ISessionStateItemCollection, ICollection, IEnumerable
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x000325B0 File Offset: 0x000307B0
		public SessionStateItemCollection() : base(Misc.CaseInsensitiveInvariantKeyComparer)
		{
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000325C8 File Offset: 0x000307C8
		static SessionStateItemCollection()
		{
			Type typeFromHandle = typeof(string);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(int);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(bool);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(DateTime);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(decimal);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(byte);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(char);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(float);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(double);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(sbyte);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(short);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(long);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(ushort);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(uint);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(ulong);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(TimeSpan);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(Guid);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(IntPtr);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
			typeFromHandle = typeof(UIntPtr);
			SessionStateItemCollection.s_immutableTypes.Add(typeFromHandle, typeFromHandle);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00032796 File Offset: 0x00030996
		internal static bool IsImmutable(object o)
		{
			return SessionStateItemCollection.s_immutableTypes[o.GetType()] != null;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000327AC File Offset: 0x000309AC
		internal void DeserializeAllItems()
		{
			if (this._serializedItems == null)
			{
				return;
			}
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				for (int i = 0; i < this._serializedItems.Count; i++)
				{
					this.DeserializeItem(this._serializedItems.GetKey(i), false);
				}
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00032818 File Offset: 0x00030A18
		private void DeserializeItem(int index)
		{
			if (this._serializedItems == null)
			{
				return;
			}
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (index < this._serializedItems.Count)
				{
					this.DeserializeItem(this._serializedItems.GetKey(index), false);
				}
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00032880 File Offset: 0x00030A80
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private object ReadValueFromStreamWithAssert()
		{
			return AltSerialization.ReadValueFromStream(new BinaryReader(this._stream));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00032894 File Offset: 0x00030A94
		private void DeserializeItem(string name, bool check)
		{
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (check)
				{
					if (this._serializedItems == null)
					{
						return;
					}
					if (!this._serializedItems.ContainsKey(name))
					{
						return;
					}
				}
				SessionStateItemCollection.SerializedItemPosition serializedItemPosition = (SessionStateItemCollection.SerializedItemPosition)this._serializedItems[name];
				if (!serializedItemPosition.IsDeserialized)
				{
					this._stream.Seek((long)serializedItemPosition.Offset, SeekOrigin.Begin);
					if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
					{
						HttpRuntime.NamedPermissionSet.PermitOnly();
					}
					object value = this.ReadValueFromStreamWithAssert();
					base.BaseSet(name, value);
					serializedItemPosition.MarkDeserializedOffsetAndCheck();
				}
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00032954 File Offset: 0x00030B54
		private void MarkItemDeserialized(string name)
		{
			if (this._serializedItems == null)
			{
				return;
			}
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (this._serializedItems.ContainsKey(name))
				{
					((SessionStateItemCollection.SerializedItemPosition)this._serializedItems[name]).MarkDeserializedOffset();
				}
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000329BC File Offset: 0x00030BBC
		private void MarkItemDeserialized(int index)
		{
			if (this._serializedItems == null)
			{
				return;
			}
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (index < this._serializedItems.Count)
				{
					((SessionStateItemCollection.SerializedItemPosition)this._serializedItems[index]).MarkDeserializedOffset();
				}
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00032A28 File Offset: 0x00030C28
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x00032A30 File Offset: 0x00030C30
		public bool Dirty
		{
			get
			{
				return this._dirty;
			}
			set
			{
				this._dirty = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		public object this[string name]
		{
			get
			{
				this.DeserializeItem(name, true);
				object obj = base.BaseGet(name);
				if (obj != null && !SessionStateItemCollection.IsImmutable(obj))
				{
					this._dirty = true;
				}
				return obj;
			}
			set
			{
				this.MarkItemDeserialized(name);
				base.BaseSet(name, value);
				this._dirty = true;
			}
		}

		// Token: 0x170005BE RID: 1470
		public object this[int index]
		{
			get
			{
				this.DeserializeItem(index);
				object obj = base.BaseGet(index);
				if (obj != null && !SessionStateItemCollection.IsImmutable(obj))
				{
					this._dirty = true;
				}
				return obj;
			}
			set
			{
				this.MarkItemDeserialized(index);
				base.BaseSet(index, value);
				this._dirty = true;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00032ACC File Offset: 0x00030CCC
		public void Remove(string name)
		{
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (this._serializedItems != null)
				{
					this._serializedItems.Remove(name);
				}
				base.BaseRemove(name);
				this._dirty = true;
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00032B28 File Offset: 0x00030D28
		public void RemoveAt(int index)
		{
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (this._serializedItems != null && index < this._serializedItems.Count)
				{
					this._serializedItems.RemoveAt(index);
				}
				base.BaseRemoveAt(index);
				this._dirty = true;
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00032B94 File Offset: 0x00030D94
		public void Clear()
		{
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				if (this._serializedItems != null)
				{
					this._serializedItems.Clear();
				}
				base.BaseClear();
				this._dirty = true;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00032BF0 File Offset: 0x00030DF0
		public override IEnumerator GetEnumerator()
		{
			this.DeserializeAllItems();
			return base.GetEnumerator();
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00032BFE File Offset: 0x00030DFE
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.DeserializeAllItems();
				return base.Keys;
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00032C0C File Offset: 0x00030E0C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private void WriteValueToStreamWithAssert(object value, BinaryWriter writer)
		{
			AltSerialization.WriteValueToStream(value, writer);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00032C18 File Offset: 0x00030E18
		public void Serialize(BinaryWriter writer)
		{
			byte[] array = null;
			Stream baseStream = writer.BaseStream;
			if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
			{
				HttpRuntime.NamedPermissionSet.PermitOnly();
			}
			object serializedItemsLock = this._serializedItemsLock;
			lock (serializedItemsLock)
			{
				int count = this.Count;
				writer.Write(count);
				if (count > 0)
				{
					if (base.BaseGet(null) != null)
					{
						for (int i = 0; i < count; i++)
						{
							if (base.BaseGetKey(i) == null)
							{
								writer.Write(i);
								break;
							}
						}
					}
					else
					{
						writer.Write(-1);
					}
					for (int i = 0; i < count; i++)
					{
						string text = base.BaseGetKey(i);
						if (text != null)
						{
							writer.Write(text);
						}
					}
					long position = baseStream.Position;
					baseStream.Seek((long)(4 * count), SeekOrigin.Current);
					long position2 = baseStream.Position;
					for (int i = 0; i < count; i++)
					{
						if (this._serializedItems != null && i < this._serializedItems.Count && !((SessionStateItemCollection.SerializedItemPosition)this._serializedItems[i]).IsDeserialized)
						{
							SessionStateItemCollection.SerializedItemPosition serializedItemPosition = (SessionStateItemCollection.SerializedItemPosition)this._serializedItems[i];
							this._stream.Seek((long)serializedItemPosition.Offset, SeekOrigin.Begin);
							if (array == null || array.Length < serializedItemPosition.DataLength)
							{
								array = new byte[serializedItemPosition.DataLength];
							}
							this._stream.Read(array, 0, serializedItemPosition.DataLength);
							baseStream.Write(array, 0, serializedItemPosition.DataLength);
						}
						else
						{
							object value = base.BaseGet(i);
							this.WriteValueToStreamWithAssert(value, writer);
						}
						long position3 = baseStream.Position;
						baseStream.Seek((long)(i * 4) + position, SeekOrigin.Begin);
						writer.Write((int)(position3 - position2));
						baseStream.Seek(position3, SeekOrigin.Begin);
					}
				}
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00032E0C File Offset: 0x0003100C
		public static SessionStateItemCollection Deserialize(BinaryReader reader)
		{
			SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection();
			int num = reader.ReadInt32();
			if (num > 0)
			{
				int num2 = reader.ReadInt32();
				sessionStateItemCollection._serializedItems = new SessionStateItemCollection.KeyedCollection(num);
				for (int i = 0; i < num; i++)
				{
					string name;
					if (i == num2)
					{
						name = null;
					}
					else
					{
						name = reader.ReadString();
					}
					sessionStateItemCollection.BaseSet(name, null);
				}
				int num3 = reader.ReadInt32();
				sessionStateItemCollection._serializedItems[sessionStateItemCollection.BaseGetKey(0)] = new SessionStateItemCollection.SerializedItemPosition(0, num3);
				for (int i = 1; i < num; i++)
				{
					int num4 = reader.ReadInt32();
					sessionStateItemCollection._serializedItems[sessionStateItemCollection.BaseGetKey(i)] = new SessionStateItemCollection.SerializedItemPosition(num3, num4 - num3);
					num3 = num4;
				}
				sessionStateItemCollection._iLastOffset = num3;
				byte[] buffer = new byte[sessionStateItemCollection._iLastOffset];
				int num5 = reader.BaseStream.Read(buffer, 0, sessionStateItemCollection._iLastOffset);
				if (num5 != sessionStateItemCollection._iLastOffset)
				{
					throw new HttpException(SR.GetString("Invalid_session_state"));
				}
				sessionStateItemCollection._stream = new MemoryStream(buffer);
			}
			sessionStateItemCollection._dirty = false;
			return sessionStateItemCollection;
		}

		// Token: 0x0400142C RID: 5164
		private static Hashtable s_immutableTypes = new Hashtable(19);

		// Token: 0x0400142D RID: 5165
		private const int NO_NULL_KEY = -1;

		// Token: 0x0400142E RID: 5166
		private const int SIZE_OF_INT32 = 4;

		// Token: 0x0400142F RID: 5167
		private bool _dirty;

		// Token: 0x04001430 RID: 5168
		private SessionStateItemCollection.KeyedCollection _serializedItems;

		// Token: 0x04001431 RID: 5169
		private Stream _stream;

		// Token: 0x04001432 RID: 5170
		private int _iLastOffset;

		// Token: 0x04001433 RID: 5171
		private object _serializedItemsLock = new object();

		// Token: 0x020008FE RID: 2302
		private class KeyedCollection : NameObjectCollectionBase
		{
			// Token: 0x060068A9 RID: 26793 RVA: 0x00174C56 File Offset: 0x00172E56
			internal KeyedCollection(int count) : base(count, Misc.CaseInsensitiveInvariantKeyComparer)
			{
			}

			// Token: 0x17001D0A RID: 7434
			internal object this[string name]
			{
				get
				{
					return base.BaseGet(name);
				}
				set
				{
					if (base.BaseGet(name) == null && value == null)
					{
						return;
					}
					base.BaseSet(name, value);
				}
			}

			// Token: 0x17001D0B RID: 7435
			internal object this[int index]
			{
				get
				{
					return base.BaseGet(index);
				}
			}

			// Token: 0x060068AD RID: 26797 RVA: 0x00174C88 File Offset: 0x00172E88
			internal void Remove(string name)
			{
				base.BaseRemove(name);
			}

			// Token: 0x060068AE RID: 26798 RVA: 0x0013DBAE File Offset: 0x0013BDAE
			internal void RemoveAt(int index)
			{
				base.BaseRemoveAt(index);
			}

			// Token: 0x060068AF RID: 26799 RVA: 0x0013DB1D File Offset: 0x0013BD1D
			internal void Clear()
			{
				base.BaseClear();
			}

			// Token: 0x060068B0 RID: 26800 RVA: 0x000166A9 File Offset: 0x000148A9
			internal string GetKey(int index)
			{
				return base.BaseGetKey(index);
			}

			// Token: 0x060068B1 RID: 26801 RVA: 0x00174C91 File Offset: 0x00172E91
			internal bool ContainsKey(string name)
			{
				return base.BaseGet(name) != null;
			}
		}

		// Token: 0x020008FF RID: 2303
		private class SerializedItemPosition
		{
			// Token: 0x060068B2 RID: 26802 RVA: 0x00174C9D File Offset: 0x00172E9D
			internal SerializedItemPosition(int offset, int dataLength)
			{
				this._offset = offset;
				this._dataLength = dataLength;
			}

			// Token: 0x17001D0C RID: 7436
			// (get) Token: 0x060068B3 RID: 26803 RVA: 0x00174CB3 File Offset: 0x00172EB3
			internal int Offset
			{
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17001D0D RID: 7437
			// (get) Token: 0x060068B4 RID: 26804 RVA: 0x00174CBB File Offset: 0x00172EBB
			internal int DataLength
			{
				get
				{
					return this._dataLength;
				}
			}

			// Token: 0x060068B5 RID: 26805 RVA: 0x00174CC3 File Offset: 0x00172EC3
			internal void MarkDeserializedOffset()
			{
				this._offset = -1;
			}

			// Token: 0x060068B6 RID: 26806 RVA: 0x00174CCC File Offset: 0x00172ECC
			internal void MarkDeserializedOffsetAndCheck()
			{
				if (this._offset >= 0)
				{
					this.MarkDeserializedOffset();
				}
			}

			// Token: 0x17001D0E RID: 7438
			// (get) Token: 0x060068B7 RID: 26807 RVA: 0x00174CDD File Offset: 0x00172EDD
			internal bool IsDeserialized
			{
				get
				{
					return this._offset < 0;
				}
			}

			// Token: 0x040036E9 RID: 14057
			private int _offset;

			// Token: 0x040036EA RID: 14058
			private int _dataLength;
		}
	}
}
