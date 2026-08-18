using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms
{
	// Token: 0x0200038F RID: 911
	[ListBindable(false)]
	[DesignerSerializer("System.Windows.Forms.Design.TableLayoutControlCollectionCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TableLayoutControlCollection : Control.ControlCollection
	{
		// Token: 0x06003BE3 RID: 15331 RVA: 0x00106090 File Offset: 0x00104290
		public TableLayoutControlCollection(TableLayoutPanel container) : base(container)
		{
			this._container = container;
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x001060A0 File Offset: 0x001042A0
		public TableLayoutPanel Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x001060A8 File Offset: 0x001042A8
		public virtual void Add(Control control, int column, int row)
		{
			base.Add(control);
			this._container.SetColumn(control, column);
			this._container.SetRow(control, row);
		}

		// Token: 0x04002386 RID: 9094
		private TableLayoutPanel _container;
	}
}
