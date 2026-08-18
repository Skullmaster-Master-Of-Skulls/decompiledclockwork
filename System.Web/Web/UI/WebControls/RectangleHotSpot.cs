using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000628 RID: 1576
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RectangleHotSpot : HotSpot
	{
		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x0013C774 File Offset: 0x0013B774
		// (set) Token: 0x06004E17 RID: 19991 RVA: 0x0013C79D File Offset: 0x0013B79D
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Bottom")]
		public int Bottom
		{
			get
			{
				object obj = base.ViewState["Bottom"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0013C7B8 File Offset: 0x0013B7B8
		// (set) Token: 0x06004E19 RID: 19993 RVA: 0x0013C7E1 File Offset: 0x0013B7E1
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Left")]
		[WebCategory("Appearance")]
		public int Left
		{
			get
			{
				object obj = base.ViewState["Left"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0013C7FC File Offset: 0x0013B7FC
		// (set) Token: 0x06004E1B RID: 19995 RVA: 0x0013C825 File Offset: 0x0013B825
		[WebSysDescription("RectangleHotSpot_Right")]
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		public int Right
		{
			get
			{
				object obj = base.ViewState["Right"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x0013C840 File Offset: 0x0013B840
		// (set) Token: 0x06004E1D RID: 19997 RVA: 0x0013C869 File Offset: 0x0013B869
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Top")]
		public int Top
		{
			get
			{
				object obj = base.ViewState["Top"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x0013C881 File Offset: 0x0013B881
		protected internal override string MarkupName
		{
			get
			{
				return "rect";
			}
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x0013C888 File Offset: 0x0013B888
		public override string GetCoordinates()
		{
			return string.Concat(new object[]
			{
				this.Left,
				",",
				this.Top,
				",",
				this.Right,
				",",
				this.Bottom
			});
		}
	}
}
