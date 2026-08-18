using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200026A RID: 618
	public abstract class GridItem
	{
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000B8F93 File Offset: 0x000B7193
		// (set) Token: 0x0600279C RID: 10140 RVA: 0x000B8F9B File Offset: 0x000B719B
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x0600279D RID: 10141
		public abstract GridItemCollection GridItems { get; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600279E RID: 10142
		public abstract GridItemType GridItemType { get; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600279F RID: 10143
		public abstract string Label { get; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060027A0 RID: 10144
		public abstract GridItem Parent { get; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060027A1 RID: 10145
		public abstract PropertyDescriptor PropertyDescriptor { get; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060027A2 RID: 10146
		public abstract object Value { get; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool Expandable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x000B8FA4 File Offset: 0x000B71A4
		public virtual bool Expanded
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("GridItemNotExpandable"));
			}
		}

		// Token: 0x060027A6 RID: 10150
		public abstract bool Select();

		// Token: 0x04001059 RID: 4185
		private object userData;
	}
}
