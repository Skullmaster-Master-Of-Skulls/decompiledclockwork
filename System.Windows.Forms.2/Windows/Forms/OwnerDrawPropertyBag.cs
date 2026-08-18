using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000316 RID: 790
	[Serializable]
	public class OwnerDrawPropertyBag : MarshalByRefObject, ISerializable
	{
		// Token: 0x06003229 RID: 12841 RVA: 0x000E1B0C File Offset: 0x000DFD0C
		protected OwnerDrawPropertyBag(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Font")
				{
					this.font = (Font)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "ForeColor")
				{
					this.foreColor = (Color)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "BackColor")
				{
					this.backColor = (Color)serializationEntry.Value;
				}
			}
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000E1BC3 File Offset: 0x000DFDC3
		internal OwnerDrawPropertyBag()
		{
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000E1BE1 File Offset: 0x000DFDE1
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x000E1BE9 File Offset: 0x000DFDE9
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000E1BF2 File Offset: 0x000DFDF2
		// (set) Token: 0x0600322E RID: 12846 RVA: 0x000E1BFA File Offset: 0x000DFDFA
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				this.foreColor = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000E1C03 File Offset: 0x000DFE03
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x000E1C0B File Offset: 0x000DFE0B
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000E1C14 File Offset: 0x000DFE14
		internal IntPtr FontHandle
		{
			get
			{
				if (this.fontWrapper == null)
				{
					this.fontWrapper = new Control.FontHandleWrapper(this.Font);
				}
				return this.fontWrapper.Handle;
			}
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000E1C3A File Offset: 0x000DFE3A
		public virtual bool IsEmpty()
		{
			return this.Font == null && this.foreColor.IsEmpty && this.backColor.IsEmpty;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000E1C60 File Offset: 0x000DFE60
		public static OwnerDrawPropertyBag Copy(OwnerDrawPropertyBag value)
		{
			object obj = OwnerDrawPropertyBag.internalSyncObject;
			OwnerDrawPropertyBag result;
			lock (obj)
			{
				OwnerDrawPropertyBag ownerDrawPropertyBag = new OwnerDrawPropertyBag();
				if (value == null)
				{
					result = ownerDrawPropertyBag;
				}
				else
				{
					ownerDrawPropertyBag.backColor = value.backColor;
					ownerDrawPropertyBag.foreColor = value.foreColor;
					ownerDrawPropertyBag.Font = value.font;
					result = ownerDrawPropertyBag;
				}
			}
			return result;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000E1CD0 File Offset: 0x000DFED0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("BackColor", this.BackColor);
			si.AddValue("ForeColor", this.ForeColor);
			si.AddValue("Font", this.Font);
		}

		// Token: 0x04001E6D RID: 7789
		private Font font;

		// Token: 0x04001E6E RID: 7790
		private Color foreColor = Color.Empty;

		// Token: 0x04001E6F RID: 7791
		private Color backColor = Color.Empty;

		// Token: 0x04001E70 RID: 7792
		private Control.FontHandleWrapper fontWrapper;

		// Token: 0x04001E71 RID: 7793
		private static object internalSyncObject = new object();
	}
}
