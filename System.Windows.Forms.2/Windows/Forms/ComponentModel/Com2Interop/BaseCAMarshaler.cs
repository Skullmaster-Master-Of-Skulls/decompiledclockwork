using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000491 RID: 1169
	internal abstract class BaseCAMarshaler
	{
		// Token: 0x06004E4F RID: 20047 RVA: 0x00142B18 File Offset: 0x00140D18
		protected BaseCAMarshaler(NativeMethods.CA_STRUCT caStruct)
		{
			if (caStruct == null)
			{
				this.count = 0;
			}
			this.count = caStruct.cElems;
			this.caArrayAddress = caStruct.pElems;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00142B44 File Offset: 0x00140D44
		protected override void Finalize()
		{
			try
			{
				if (this.itemArray == null && this.caArrayAddress != IntPtr.Zero)
				{
					object[] items = this.Items;
				}
			}
			catch
			{
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06004E51 RID: 20049
		protected abstract Array CreateArray();

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004E52 RID: 20050
		public abstract Type ItemType { get; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00142B9C File Offset: 0x00140D9C
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x00142BA4 File Offset: 0x00140DA4
		public virtual object[] Items
		{
			get
			{
				try
				{
					if (this.itemArray == null)
					{
						this.itemArray = this.Get_Items();
					}
				}
				catch (Exception ex)
				{
				}
				return this.itemArray;
			}
		}

		// Token: 0x06004E55 RID: 20053
		protected abstract object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex);

		// Token: 0x06004E56 RID: 20054 RVA: 0x00142BE0 File Offset: 0x00140DE0
		private object[] Get_Items()
		{
			Array array = new object[this.Count];
			for (int i = 0; i < this.count; i++)
			{
				try
				{
					object obj = this.UnmarshalAndFreeOneItem(this.caArrayAddress, i);
					if (obj != null && this.ItemType.IsInstanceOfType(obj))
					{
						array.SetValue(obj, i);
					}
				}
				catch (Exception ex)
				{
				}
			}
			Marshal.FreeCoTaskMem(this.caArrayAddress);
			this.caArrayAddress = IntPtr.Zero;
			return (object[])array;
		}

		// Token: 0x04003406 RID: 13318
		private static TraceSwitch CAMarshalSwitch = new TraceSwitch("CAMarshal", "BaseCAMarshaler: Debug CA* struct marshaling");

		// Token: 0x04003407 RID: 13319
		private IntPtr caArrayAddress;

		// Token: 0x04003408 RID: 13320
		private int count;

		// Token: 0x04003409 RID: 13321
		private object[] itemArray;
	}
}
