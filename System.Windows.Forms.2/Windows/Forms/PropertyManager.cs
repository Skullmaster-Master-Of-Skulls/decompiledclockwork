using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000331 RID: 817
	public class PropertyManager : BindingManagerBase
	{
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000F18E7 File Offset: 0x000EFAE7
		public override object Current
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000F18EF File Offset: 0x000EFAEF
		private void PropertyChanged(object sender, EventArgs ea)
		{
			this.EndCurrentEdit();
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000F1904 File Offset: 0x000EFB04
		internal override void SetDataSource(object dataSource)
		{
			if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
			{
				this.propInfo.RemoveValueChanged(this.dataSource, new EventHandler(this.PropertyChanged));
				this.propInfo = null;
			}
			this.dataSource = dataSource;
			if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
			{
				this.propInfo = TypeDescriptor.GetProperties(dataSource).Find(this.propName, true);
				if (this.propInfo == null)
				{
					throw new ArgumentException(SR.GetString("PropertyManagerPropDoesNotExist", new object[]
					{
						this.propName,
						dataSource.ToString()
					}));
				}
				this.propInfo.AddValueChanged(dataSource, new EventHandler(this.PropertyChanged));
			}
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000F19C6 File Offset: 0x000EFBC6
		public PropertyManager()
		{
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000F19CE File Offset: 0x000EFBCE
		internal PropertyManager(object dataSource) : base(dataSource)
		{
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000F19D7 File Offset: 0x000EFBD7
		internal PropertyManager(object dataSource, string propName)
		{
			this.propName = propName;
			this.SetDataSource(dataSource);
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000F19ED File Offset: 0x000EFBED
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListItemProperties(this.dataSource, listAccessors);
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000F19FB File Offset: 0x000EFBFB
		internal override Type BindType
		{
			get
			{
				return this.dataSource.GetType();
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000F1A08 File Offset: 0x000EFC08
		internal override string GetListName()
		{
			return TypeDescriptor.GetClassName(this.dataSource) + "." + this.propName;
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000F1A28 File Offset: 0x000EFC28
		public override void SuspendBinding()
		{
			this.EndCurrentEdit();
			if (this.bound)
			{
				try
				{
					this.bound = false;
					this.UpdateIsBinding();
				}
				catch
				{
					this.bound = true;
					this.UpdateIsBinding();
					throw;
				}
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000F1A74 File Offset: 0x000EFC74
		public override void ResumeBinding()
		{
			this.OnCurrentChanged(new EventArgs());
			if (!this.bound)
			{
				try
				{
					this.bound = true;
					this.UpdateIsBinding();
				}
				catch
				{
					this.bound = false;
					this.UpdateIsBinding();
					throw;
				}
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000F1AC4 File Offset: 0x000EFCC4
		protected internal override string GetListName(ArrayList listAccessors)
		{
			return "";
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000F1ACC File Offset: 0x000EFCCC
		public override void CancelCurrentEdit()
		{
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				editableObject.CancelEdit();
			}
			base.PushData();
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000F1AF4 File Offset: 0x000EFCF4
		public override void EndCurrentEdit()
		{
			bool flag;
			base.PullData(out flag);
			if (flag)
			{
				IEditableObject editableObject = this.Current as IEditableObject;
				if (editableObject != null)
				{
					editableObject.EndEdit();
				}
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000F1B24 File Offset: 0x000EFD24
		protected override void UpdateIsBinding()
		{
			for (int i = 0; i < base.Bindings.Count; i++)
			{
				base.Bindings[i].UpdateIsBinding();
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000F1B58 File Offset: 0x000EFD58
		protected internal override void OnCurrentChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler(this, ea);
			}
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, ea);
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000F1B8A File Offset: 0x000EFD8A
		protected internal override void OnCurrentItemChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, ea);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000F18E7 File Offset: 0x000EFAE7
		internal override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000F1BA7 File Offset: 0x000EFDA7
		internal override bool IsBinding
		{
			get
			{
				return this.dataSource != null;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000072B6 File Offset: 0x000054B6
		public override int Position
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x00013062 File Offset: 0x00011262
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000F1BB2 File Offset: 0x000EFDB2
		public override void AddNew()
		{
			throw new NotSupportedException(SR.GetString("DataBindingAddNewNotSupportedOnPropertyManager"));
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000F1BC3 File Offset: 0x000EFDC3
		public override void RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataBindingRemoveAtNotSupportedOnPropertyManager"));
		}

		// Token: 0x04001F47 RID: 8007
		private object dataSource;

		// Token: 0x04001F48 RID: 8008
		private string propName;

		// Token: 0x04001F49 RID: 8009
		private PropertyDescriptor propInfo;

		// Token: 0x04001F4A RID: 8010
		private bool bound;
	}
}
