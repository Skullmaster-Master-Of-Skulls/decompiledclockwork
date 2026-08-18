using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;

namespace Spire.DataExport.Designers
{
	// Token: 0x02000231 RID: 561
	public class ExportDesigner : ComponentDesigner
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x000B6310 File Offset: 0x000B5310
		protected void CreateVerbs()
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ = new DesignerVerbCollection();
			this.ᜀ.Add(new DesignerVerb(HyperlinksCollectionEditor.b("С朣䤥䐧弩䄫䀭䌯ሱ焳刵儷丹医䰽", a_), new EventHandler(this.ᜁ)));
			this.ᜀ.Add(new DesignerVerb(HyperlinksCollectionEditor.b("С攣䐥䜧弩堫อ振䈱崳䐵崷ᐹ砻弽㐿⍁Ń㹅㡇╉㹋㩍", a_), new EventHandler(this.ᜀ)));
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000B63B8 File Offset: 0x000B53B8
		private void ᜁ(object A_0, EventArgs A_1)
		{
			int a_ = 18;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (DataExportColumnsEditor.RunDataExportColumnsEditor((base.Component as ExportBase).DataSource, (base.Component as ExportBase).SQLCommand, (base.Component as ExportBase).DataTable, (base.Component as ExportBase).ListView, (base.Component as ExportBase).Columns))
					{
						num = 1;
						continue;
					}
					return;
				case 1:
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((base.Component as ExportBase).GetType());
					PropertyDescriptor propertyDescriptor = properties.Find(HyperlinksCollectionEditor.b("欭䠯䈱嬳䐵䰷弹堻砽⤿❁⡃≅㭇", a_), false);
					num = 6;
					continue;
				}
				case 2:
				{
					PropertyDescriptor propertyDescriptor;
					base.RaiseComponentChanged(propertyDescriptor, null, (base.Component as ExportBase).Columns);
					num = 4;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 4:
					return;
				case 6:
				{
					PropertyDescriptor propertyDescriptor;
					if (propertyDescriptor != null)
					{
						num = 2;
						continue;
					}
					return;
				}
				}
				if (!(base.Component is ExportBase))
				{
					break;
				}
				num = 3;
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000B6524 File Offset: 0x000B5524
		private void ᜀ(object A_0, EventArgs A_1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			AboutDataExport.ShowAbout(true);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000B6568 File Offset: 0x000B5568
		public override DesignerVerbCollection Verbs
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.CreateVerbs();
						goto IL_34;
					case 2:
						goto IL_3E;
					}
					if (this.ᜀ == null)
					{
						num = 1;
						continue;
					}
					goto IL_3E;
					IL_34:
					num = 2;
					continue;
					IL_3E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_54;
					}
				}
				IL_54:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x04000C14 RID: 3092
		private long \u25D9\u008D\u00B0\u0083;

		// Token: 0x04000C15 RID: 3093
		private int \u2609\u0097\u00AC\u00AF;

		// Token: 0x04000C16 RID: 3094
		private byte \u25D8\u0081\u0081\u00A0;

		// Token: 0x04000C17 RID: 3095
		private DesignerVerbCollection ᜀ;
	}
}
