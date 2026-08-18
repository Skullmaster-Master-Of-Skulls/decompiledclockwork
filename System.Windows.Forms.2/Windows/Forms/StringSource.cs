using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	// Token: 0x02000381 RID: 897
	internal class StringSource : IEnumString
	{
		// Token: 0x06003A99 RID: 15001 RVA: 0x00102590 File Offset: 0x00100790
		public StringSource(string[] strings)
		{
			Array.Clear(strings, 0, this.size);
			if (strings != null)
			{
				this.strings = strings;
			}
			this.current = 0;
			this.size = ((strings == null) ? 0 : strings.Length);
			Guid guid = typeof(UnsafeNativeMethods.IAutoComplete2).GUID;
			object obj = UnsafeNativeMethods.CoCreateInstance(ref StringSource.autoCompleteClsid, null, 1, ref guid);
			this.autoCompleteObject2 = (UnsafeNativeMethods.IAutoComplete2)obj;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x001025FC File Offset: 0x001007FC
		public bool Bind(HandleRef edit, int options)
		{
			bool result = false;
			if (this.autoCompleteObject2 != null)
			{
				try
				{
					this.autoCompleteObject2.SetOptions(options);
					this.autoCompleteObject2.Init(edit, this, null, null);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0010264C File Offset: 0x0010084C
		public void ReleaseAutoComplete()
		{
			if (this.autoCompleteObject2 != null)
			{
				Marshal.ReleaseComObject(this.autoCompleteObject2);
				this.autoCompleteObject2 = null;
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x0010266C File Offset: 0x0010086C
		public void RefreshList(string[] newSource)
		{
			Array.Clear(this.strings, 0, this.size);
			if (this.strings != null)
			{
				this.strings = newSource;
			}
			this.current = 0;
			this.size = ((this.strings == null) ? 0 : this.strings.Length);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x001026BA File Offset: 0x001008BA
		void IEnumString.Clone(out IEnumString ppenum)
		{
			ppenum = new StringSource(this.strings);
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x001026CC File Offset: 0x001008CC
		int IEnumString.Next(int celt, string[] rgelt, IntPtr pceltFetched)
		{
			if (celt < 0)
			{
				return -2147024809;
			}
			int num = 0;
			while (this.current < this.size && celt > 0)
			{
				rgelt[num] = this.strings[this.current];
				this.current++;
				num++;
				celt--;
			}
			if (pceltFetched != IntPtr.Zero)
			{
				Marshal.WriteInt32(pceltFetched, num);
			}
			if (celt != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0010273A File Offset: 0x0010093A
		void IEnumString.Reset()
		{
			this.current = 0;
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x00102743 File Offset: 0x00100943
		int IEnumString.Skip(int celt)
		{
			this.current += celt;
			if (this.current >= this.size)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04002326 RID: 8998
		private string[] strings;

		// Token: 0x04002327 RID: 8999
		private int current;

		// Token: 0x04002328 RID: 9000
		private int size;

		// Token: 0x04002329 RID: 9001
		private UnsafeNativeMethods.IAutoComplete2 autoCompleteObject2;

		// Token: 0x0400232A RID: 9002
		private static Guid autoCompleteClsid = new Guid("{00BB2763-6A77-11D0-A535-00C04FD7D062}");
	}
}
