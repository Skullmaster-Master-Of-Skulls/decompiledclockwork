using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
	// Token: 0x02000089 RID: 137
	public sealed class PrivateFontCollection : FontCollection
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x00022140 File Offset: 0x00020340
		public PrivateFontCollection()
		{
			this.nativeFontCollection = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipNewPrivateFontCollection(out this.nativeFontCollection);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			if (!LocalAppContextSwitches.DoNotRemoveGdiFontsResourcesFromFontCollection)
			{
				this.gdiFonts = new List<string>();
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00022188 File Offset: 0x00020388
		protected override void Dispose(bool disposing)
		{
			if (this.nativeFontCollection != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeletePrivateFontCollection(out this.nativeFontCollection);
					if (this.gdiFonts != null)
					{
						foreach (string fileName in this.gdiFonts)
						{
							SafeNativeMethods.RemoveFontFile(fileName);
						}
						this.gdiFonts.Clear();
						this.gdiFonts = null;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeFontCollection = IntPtr.Zero;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00022250 File Offset: 0x00020450
		public void AddFontFile(string filename)
		{
			IntSecurity.DemandReadFileIO(filename);
			int num = SafeNativeMethods.Gdip.GdipPrivateAddFontFile(new HandleRef(this, this.nativeFontCollection), filename);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			if (SafeNativeMethods.AddFontFile(filename) != 0 && this.gdiFonts != null)
			{
				this.gdiFonts.Add(filename);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002229C File Offset: 0x0002049C
		public void AddMemoryFont(IntPtr memory, int length)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			int num = SafeNativeMethods.Gdip.GdipPrivateAddMemoryFont(new HandleRef(this, this.nativeFontCollection), new HandleRef(null, memory), length);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x04000729 RID: 1833
		private List<string> gdiFonts;
	}
}
