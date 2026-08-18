using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FF RID: 2303
	internal abstract class NativeMsmqMessage : IDisposable
	{
		// Token: 0x060057E4 RID: 22500 RVA: 0x0014315C File Offset: 0x0014135C
		protected NativeMsmqMessage(int propertyCount)
		{
			this.properties = new NativeMsmqMessage.MsmqProperty[propertyCount];
			this.nativeProperties = new UnsafeNativeMethods.MQMSGPROPS();
			this.ids = new int[propertyCount];
			this.variants = new UnsafeNativeMethods.MQPROPVARIANT[propertyCount];
			this.nativePropertiesHandle = GCHandle.Alloc(null, GCHandleType.Pinned);
			this.idsHandle = GCHandle.Alloc(null, GCHandleType.Pinned);
			this.variantsHandle = GCHandle.Alloc(null, GCHandleType.Pinned);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001431C8 File Offset: 0x001413C8
		~NativeMsmqMessage()
		{
			this.Dispose(false);
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x001431F8 File Offset: 0x001413F8
		public virtual void GrowBuffers()
		{
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x001431FC File Offset: 0x001413FC
		public object[] GetBuffersForAsync()
		{
			if (this.buffersForAsync == null)
			{
				int num = 0;
				for (int i = 0; i < this.nativeProperties.count; i++)
				{
					if (this.properties[i].MaintainsBuffer)
					{
						num++;
					}
				}
				this.buffersForAsync = new object[num + 3];
			}
			int num2 = 0;
			for (int j = 0; j < this.nativeProperties.count; j++)
			{
				if (this.properties[j].MaintainsBuffer)
				{
					this.buffersForAsync[num2++] = this.properties[j].MaintainedBuffer;
				}
			}
			this.buffersForAsync[num2++] = this.ids;
			this.buffersForAsync[num2++] = this.variants;
			this.buffersForAsync[num2] = this.nativeProperties;
			return this.buffersForAsync;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x001432C4 File Offset: 0x001414C4
		public IntPtr Pin()
		{
			for (int i = 0; i < this.nativeProperties.count; i++)
			{
				this.properties[i].Pin();
			}
			this.idsHandle.Target = this.ids;
			this.variantsHandle.Target = this.variants;
			this.nativeProperties.status = IntPtr.Zero;
			this.nativeProperties.variants = this.variantsHandle.AddrOfPinnedObject();
			this.nativeProperties.ids = this.idsHandle.AddrOfPinnedObject();
			this.nativePropertiesHandle.Target = this.nativeProperties;
			return this.nativePropertiesHandle.AddrOfPinnedObject();
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x00143370 File Offset: 0x00141570
		public void Unpin()
		{
			this.nativePropertiesHandle.Target = null;
			this.idsHandle.Target = null;
			this.variantsHandle.Target = null;
			for (int i = 0; i < this.nativeProperties.count; i++)
			{
				this.properties[i].Unpin();
			}
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001433C4 File Offset: 0x001415C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001433D4 File Offset: 0x001415D4
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				for (int i = 0; i < this.nativeProperties.count; i++)
				{
					this.properties[i].Dispose();
				}
				this.disposed = true;
			}
			if (this.nativePropertiesHandle.IsAllocated)
			{
				this.nativePropertiesHandle.Free();
			}
			if (this.idsHandle.IsAllocated)
			{
				this.idsHandle.Free();
			}
			if (this.variantsHandle.IsAllocated)
			{
				this.variantsHandle.Free();
			}
		}

		// Token: 0x04003601 RID: 13825
		private UnsafeNativeMethods.MQPROPVARIANT[] variants;

		// Token: 0x04003602 RID: 13826
		private UnsafeNativeMethods.MQMSGPROPS nativeProperties;

		// Token: 0x04003603 RID: 13827
		private int[] ids;

		// Token: 0x04003604 RID: 13828
		private GCHandle nativePropertiesHandle;

		// Token: 0x04003605 RID: 13829
		private GCHandle variantsHandle;

		// Token: 0x04003606 RID: 13830
		private GCHandle idsHandle;

		// Token: 0x04003607 RID: 13831
		private NativeMsmqMessage.MsmqProperty[] properties;

		// Token: 0x04003608 RID: 13832
		private bool disposed;

		// Token: 0x04003609 RID: 13833
		private object[] buffersForAsync;

		// Token: 0x02000DA7 RID: 3495
		public abstract class MsmqProperty : IDisposable
		{
			// Token: 0x06007EF2 RID: 32498 RVA: 0x001D8D60 File Offset: 0x001D6F60
			protected MsmqProperty(NativeMsmqMessage message, int id, ushort vt)
			{
				this.variants = message.variants;
				UnsafeNativeMethods.MQMSGPROPS nativeProperties = message.nativeProperties;
				int count = nativeProperties.count;
				nativeProperties.count = count + 1;
				this.index = count;
				message.variants[this.index].vt = vt;
				message.ids[this.index] = id;
				message.properties[this.index] = this;
			}

			// Token: 0x17001C46 RID: 7238
			// (get) Token: 0x06007EF3 RID: 32499 RVA: 0x001D8DCE File Offset: 0x001D6FCE
			protected int Index
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x17001C47 RID: 7239
			// (get) Token: 0x06007EF4 RID: 32500 RVA: 0x001D8DD6 File Offset: 0x001D6FD6
			public virtual bool MaintainsBuffer
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001C48 RID: 7240
			// (get) Token: 0x06007EF5 RID: 32501 RVA: 0x001D8DD9 File Offset: 0x001D6FD9
			public virtual object MaintainedBuffer
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007EF6 RID: 32502 RVA: 0x001D8DDC File Offset: 0x001D6FDC
			public virtual void Pin()
			{
			}

			// Token: 0x06007EF7 RID: 32503 RVA: 0x001D8DDE File Offset: 0x001D6FDE
			public virtual void Unpin()
			{
			}

			// Token: 0x06007EF8 RID: 32504 RVA: 0x001D8DE0 File Offset: 0x001D6FE0
			public virtual void Dispose()
			{
			}

			// Token: 0x17001C49 RID: 7241
			// (get) Token: 0x06007EF9 RID: 32505 RVA: 0x001D8DE2 File Offset: 0x001D6FE2
			protected UnsafeNativeMethods.MQPROPVARIANT[] Variants
			{
				get
				{
					return this.variants;
				}
			}

			// Token: 0x040048DC RID: 18652
			private UnsafeNativeMethods.MQPROPVARIANT[] variants;

			// Token: 0x040048DD RID: 18653
			private int index;
		}

		// Token: 0x02000DA8 RID: 3496
		public class ByteProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007EFA RID: 32506 RVA: 0x001D8DEA File Offset: 0x001D6FEA
			public ByteProperty(NativeMsmqMessage message, int id) : base(message, id, 17)
			{
			}

			// Token: 0x06007EFB RID: 32507 RVA: 0x001D8DF6 File Offset: 0x001D6FF6
			public ByteProperty(NativeMsmqMessage message, int id, byte value) : this(message, id)
			{
				this.Value = value;
			}

			// Token: 0x17001C4A RID: 7242
			// (get) Token: 0x06007EFC RID: 32508 RVA: 0x001D8E07 File Offset: 0x001D7007
			// (set) Token: 0x06007EFD RID: 32509 RVA: 0x001D8E1F File Offset: 0x001D701F
			public byte Value
			{
				get
				{
					return base.Variants[base.Index].byteValue;
				}
				set
				{
					base.Variants[base.Index].byteValue = value;
				}
			}
		}

		// Token: 0x02000DA9 RID: 3497
		public class ShortProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007EFE RID: 32510 RVA: 0x001D8E38 File Offset: 0x001D7038
			public ShortProperty(NativeMsmqMessage message, int id) : base(message, id, 18)
			{
			}

			// Token: 0x06007EFF RID: 32511 RVA: 0x001D8E44 File Offset: 0x001D7044
			public ShortProperty(NativeMsmqMessage message, int id, short value) : this(message, id)
			{
				this.Value = value;
			}

			// Token: 0x17001C4B RID: 7243
			// (get) Token: 0x06007F00 RID: 32512 RVA: 0x001D8E55 File Offset: 0x001D7055
			// (set) Token: 0x06007F01 RID: 32513 RVA: 0x001D8E6D File Offset: 0x001D706D
			public short Value
			{
				get
				{
					return base.Variants[base.Index].shortValue;
				}
				set
				{
					base.Variants[base.Index].shortValue = value;
				}
			}
		}

		// Token: 0x02000DAA RID: 3498
		public class BooleanProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007F02 RID: 32514 RVA: 0x001D8E86 File Offset: 0x001D7086
			public BooleanProperty(NativeMsmqMessage message, int id) : base(message, id, 11)
			{
			}

			// Token: 0x06007F03 RID: 32515 RVA: 0x001D8E92 File Offset: 0x001D7092
			public BooleanProperty(NativeMsmqMessage message, int id, bool value) : this(message, id)
			{
				this.Value = value;
			}

			// Token: 0x17001C4C RID: 7244
			// (get) Token: 0x06007F04 RID: 32516 RVA: 0x001D8EA3 File Offset: 0x001D70A3
			// (set) Token: 0x06007F05 RID: 32517 RVA: 0x001D8EBE File Offset: 0x001D70BE
			public bool Value
			{
				get
				{
					return base.Variants[base.Index].shortValue != 0;
				}
				set
				{
					base.Variants[base.Index].shortValue = (value ? -1 : 0);
				}
			}
		}

		// Token: 0x02000DAB RID: 3499
		public class IntProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007F06 RID: 32518 RVA: 0x001D8EDD File Offset: 0x001D70DD
			public IntProperty(NativeMsmqMessage message, int id) : base(message, id, 19)
			{
			}

			// Token: 0x06007F07 RID: 32519 RVA: 0x001D8EE9 File Offset: 0x001D70E9
			public IntProperty(NativeMsmqMessage message, int id, int value) : this(message, id)
			{
				this.Value = value;
			}

			// Token: 0x17001C4D RID: 7245
			// (get) Token: 0x06007F08 RID: 32520 RVA: 0x001D8EFA File Offset: 0x001D70FA
			// (set) Token: 0x06007F09 RID: 32521 RVA: 0x001D8F12 File Offset: 0x001D7112
			public int Value
			{
				get
				{
					return base.Variants[base.Index].intValue;
				}
				set
				{
					base.Variants[base.Index].intValue = value;
				}
			}
		}

		// Token: 0x02000DAC RID: 3500
		public class LongProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007F0A RID: 32522 RVA: 0x001D8F2B File Offset: 0x001D712B
			public LongProperty(NativeMsmqMessage message, int id) : base(message, id, 21)
			{
			}

			// Token: 0x06007F0B RID: 32523 RVA: 0x001D8F37 File Offset: 0x001D7137
			public LongProperty(NativeMsmqMessage message, int id, long value) : this(message, id)
			{
				this.Value = value;
			}

			// Token: 0x17001C4E RID: 7246
			// (get) Token: 0x06007F0C RID: 32524 RVA: 0x001D8F48 File Offset: 0x001D7148
			// (set) Token: 0x06007F0D RID: 32525 RVA: 0x001D8F60 File Offset: 0x001D7160
			public long Value
			{
				get
				{
					return base.Variants[base.Index].longValue;
				}
				set
				{
					base.Variants[base.Index].longValue = value;
				}
			}
		}

		// Token: 0x02000DAD RID: 3501
		public class BufferProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007F0E RID: 32526 RVA: 0x001D8F79 File Offset: 0x001D7179
			public BufferProperty(NativeMsmqMessage message, int id) : base(message, id, 4113)
			{
				this.bufferHandle = GCHandle.Alloc(null, GCHandleType.Pinned);
			}

			// Token: 0x06007F0F RID: 32527 RVA: 0x001D8F95 File Offset: 0x001D7195
			public BufferProperty(NativeMsmqMessage message, int id, byte[] buffer) : this(message, id, buffer.Length)
			{
				System.Buffer.BlockCopy(buffer, 0, this.Buffer, 0, buffer.Length);
			}

			// Token: 0x06007F10 RID: 32528 RVA: 0x001D8FB3 File Offset: 0x001D71B3
			public BufferProperty(NativeMsmqMessage message, int id, int length) : this(message, id)
			{
				this.SetBufferReference(DiagnosticUtility.Utility.AllocateByteArray(length));
			}

			// Token: 0x06007F11 RID: 32529 RVA: 0x001D8FD0 File Offset: 0x001D71D0
			~BufferProperty()
			{
				this.Dispose(false);
			}

			// Token: 0x06007F12 RID: 32530 RVA: 0x001D9000 File Offset: 0x001D7200
			public override void Dispose()
			{
				base.Dispose();
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06007F13 RID: 32531 RVA: 0x001D9015 File Offset: 0x001D7215
			private void Dispose(bool disposing)
			{
				if (this.bufferHandle.IsAllocated)
				{
					this.bufferHandle.Free();
				}
			}

			// Token: 0x06007F14 RID: 32532 RVA: 0x001D902F File Offset: 0x001D722F
			public void SetBufferReference(byte[] buffer)
			{
				this.SetBufferReference(buffer, buffer.Length);
			}

			// Token: 0x06007F15 RID: 32533 RVA: 0x001D903B File Offset: 0x001D723B
			public void SetBufferReference(byte[] buffer, int length)
			{
				this.buffer = buffer;
				this.BufferLength = length;
			}

			// Token: 0x17001C4F RID: 7247
			// (get) Token: 0x06007F16 RID: 32534 RVA: 0x001D904B File Offset: 0x001D724B
			public override bool MaintainsBuffer
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001C50 RID: 7248
			// (get) Token: 0x06007F17 RID: 32535 RVA: 0x001D904E File Offset: 0x001D724E
			public override object MaintainedBuffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x06007F18 RID: 32536 RVA: 0x001D9056 File Offset: 0x001D7256
			public override void Pin()
			{
				this.bufferHandle.Target = this.buffer;
				base.Variants[base.Index].byteArrayValue.intPtr = this.bufferHandle.AddrOfPinnedObject();
			}

			// Token: 0x06007F19 RID: 32537 RVA: 0x001D908F File Offset: 0x001D728F
			public override void Unpin()
			{
				base.Variants[base.Index].byteArrayValue.intPtr = IntPtr.Zero;
				this.bufferHandle.Target = null;
			}

			// Token: 0x06007F1A RID: 32538 RVA: 0x001D90C0 File Offset: 0x001D72C0
			public byte[] GetBufferCopy(int length)
			{
				byte[] array = DiagnosticUtility.Utility.AllocateByteArray(length);
				System.Buffer.BlockCopy(this.buffer, 0, array, 0, length);
				return array;
			}

			// Token: 0x06007F1B RID: 32539 RVA: 0x001D90E9 File Offset: 0x001D72E9
			public void EnsureBufferLength(int length)
			{
				if (this.buffer.Length < length)
				{
					this.SetBufferReference(DiagnosticUtility.Utility.AllocateByteArray(length));
				}
			}

			// Token: 0x17001C51 RID: 7249
			// (get) Token: 0x06007F1C RID: 32540 RVA: 0x001D9107 File Offset: 0x001D7307
			// (set) Token: 0x06007F1D RID: 32541 RVA: 0x001D9124 File Offset: 0x001D7324
			public int BufferLength
			{
				get
				{
					return base.Variants[base.Index].byteArrayValue.size;
				}
				set
				{
					if (value > this.buffer.Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
					}
					base.Variants[base.Index].byteArrayValue.size = value;
				}
			}

			// Token: 0x17001C52 RID: 7250
			// (get) Token: 0x06007F1E RID: 32542 RVA: 0x001D9162 File Offset: 0x001D7362
			public byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x040048DE RID: 18654
			private byte[] buffer;

			// Token: 0x040048DF RID: 18655
			private GCHandle bufferHandle;
		}

		// Token: 0x02000DAE RID: 3502
		public class StringProperty : NativeMsmqMessage.MsmqProperty
		{
			// Token: 0x06007F1F RID: 32543 RVA: 0x001D916A File Offset: 0x001D736A
			internal StringProperty(NativeMsmqMessage message, int id, string value) : this(message, id, value.Length + 1)
			{
				this.CopyValueToBuffer(value);
			}

			// Token: 0x06007F20 RID: 32544 RVA: 0x001D9183 File Offset: 0x001D7383
			internal StringProperty(NativeMsmqMessage message, int id, int length) : base(message, id, 31)
			{
				this.buffer = DiagnosticUtility.Utility.AllocateCharArray(length);
				this.bufferHandle = GCHandle.Alloc(null, GCHandleType.Pinned);
			}

			// Token: 0x06007F21 RID: 32545 RVA: 0x001D91B0 File Offset: 0x001D73B0
			~StringProperty()
			{
				this.Dispose(false);
			}

			// Token: 0x17001C53 RID: 7251
			// (get) Token: 0x06007F22 RID: 32546 RVA: 0x001D91E0 File Offset: 0x001D73E0
			public override bool MaintainsBuffer
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001C54 RID: 7252
			// (get) Token: 0x06007F23 RID: 32547 RVA: 0x001D91E3 File Offset: 0x001D73E3
			public override object MaintainedBuffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x06007F24 RID: 32548 RVA: 0x001D91EB File Offset: 0x001D73EB
			public override void Pin()
			{
				this.bufferHandle.Target = this.buffer;
				base.Variants[base.Index].intPtr = this.bufferHandle.AddrOfPinnedObject();
			}

			// Token: 0x06007F25 RID: 32549 RVA: 0x001D921F File Offset: 0x001D741F
			public override void Unpin()
			{
				base.Variants[base.Index].intPtr = IntPtr.Zero;
				this.bufferHandle.Target = null;
			}

			// Token: 0x06007F26 RID: 32550 RVA: 0x001D9248 File Offset: 0x001D7448
			public override void Dispose()
			{
				base.Dispose();
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06007F27 RID: 32551 RVA: 0x001D925D File Offset: 0x001D745D
			private void Dispose(bool disposing)
			{
				if (this.bufferHandle.IsAllocated)
				{
					this.bufferHandle.Free();
				}
			}

			// Token: 0x06007F28 RID: 32552 RVA: 0x001D9277 File Offset: 0x001D7477
			public void EnsureValueLength(int length)
			{
				if (length > this.buffer.Length)
				{
					this.buffer = DiagnosticUtility.Utility.AllocateCharArray(length);
				}
			}

			// Token: 0x06007F29 RID: 32553 RVA: 0x001D9295 File Offset: 0x001D7495
			public void SetValue(string value)
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.EnsureValueLength(value.Length + 1);
				this.CopyValueToBuffer(value);
			}

			// Token: 0x06007F2A RID: 32554 RVA: 0x001D92BF File Offset: 0x001D74BF
			private void CopyValueToBuffer(string value)
			{
				value.CopyTo(0, this.buffer, 0, value.Length);
				this.buffer[value.Length] = '\0';
			}

			// Token: 0x06007F2B RID: 32555 RVA: 0x001D92E3 File Offset: 0x001D74E3
			public string GetValue(int length)
			{
				if (length == 0)
				{
					return null;
				}
				return new string(this.buffer, 0, length - 1);
			}

			// Token: 0x040048E0 RID: 18656
			private char[] buffer;

			// Token: 0x040048E1 RID: 18657
			private GCHandle bufferHandle;
		}
	}
}
