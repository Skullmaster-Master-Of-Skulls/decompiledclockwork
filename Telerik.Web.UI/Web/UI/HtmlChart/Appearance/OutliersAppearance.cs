using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x020004CB RID: 1227
	public class OutliersAppearance : MarkersAppearanceBase
	{
		// Token: 0x06002C79 RID: 11385 RVA: 0x00092358 File Offset: 0x00090558
		public OutliersAppearance(string prefix, StateBag stateBag) : base(prefix, stateBag)
		{
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x00092362 File Offset: 0x00090562
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x00092388 File Offset: 0x00090588
		[DefaultValue(true)]
		public override bool? Visible
		{
			get
			{
				return new bool?((bool)(base.ViewState["Visible"] ?? true));
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000923A0 File Offset: 0x000905A0
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000923C1 File Offset: 0x000905C1
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override int RotationAngle
		{
			get
			{
				return (int)(base.ViewState["RotationAngle"] ?? 0);
			}
			set
			{
				base.ViewState["RotationAngle"] = value;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000923D9 File Offset: 0x000905D9
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x000923FA File Offset: 0x000905FA
		[DefaultValue(OutliersMarkersType.Cross)]
		public new OutliersMarkersType MarkersType
		{
			get
			{
				return (OutliersMarkersType)(base.ViewState["MarkersType"] ?? OutliersMarkersType.Cross);
			}
			set
			{
				base.ViewState["MarkersType"] = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x00092412 File Offset: 0x00090612
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BorderAppearance BorderAppearance
		{
			get
			{
				if (this._borderAppearance == null)
				{
					this._borderAppearance = new BorderAppearance();
				}
				return this._borderAppearance;
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0009242D File Offset: 0x0009062D
		internal override string Serialize()
		{
			return string.Format("{0}", this.Serializer.Serialize(this));
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x00092445 File Offset: 0x00090645
		protected JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this.InitSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0009245B File Offset: 0x0009065B
		protected void InitSerializer()
		{
			this._serializer = new JavaScriptSerializer();
			this.RegisterConverters();
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x00092470 File Offset: 0x00090670
		protected virtual void RegisterConverters()
		{
			this._serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new OutliersAppearanceConverter(),
				new BorderAppearanceConverter()
			});
		}

		// Token: 0x04000B7E RID: 2942
		private BorderAppearance _borderAppearance;

		// Token: 0x04000B7F RID: 2943
		private JavaScriptSerializer _serializer;
	}
}
