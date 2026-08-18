using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000510 RID: 1296
	internal class MultiPropertyDescriptorGridEntry : PropertyDescriptorGridEntry
	{
		// Token: 0x060054E5 RID: 21733 RVA: 0x00163CB1 File Offset: 0x00161EB1
		public MultiPropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, object[] objectArray, PropertyDescriptor[] propInfo, bool hide) : base(ownerGrid, peParent, hide)
		{
			this.mergedPd = new MergePropertyDescriptor(propInfo);
			this.objs = objectArray;
			base.Initialize(this.mergedPd);
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x00163CE0 File Offset: 0x00161EE0
		public override IContainer Container
		{
			get
			{
				IContainer container = null;
				object[] array = this.objs;
				int i = 0;
				while (i < array.Length)
				{
					object obj = array[i];
					IComponent component = obj as IComponent;
					if (component == null)
					{
						container = null;
						break;
					}
					if (component.Site != null)
					{
						if (container == null)
						{
							container = component.Site.Container;
						}
						else if (container != component.Site.Container)
						{
							goto IL_4B;
						}
						i++;
						continue;
					}
					IL_4B:
					container = null;
					break;
				}
				return container;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x00163D48 File Offset: 0x00161F48
		public override bool Expandable
		{
			get
			{
				bool flag = this.GetFlagSet(131072);
				if (flag && base.ChildCollection.Count > 0)
				{
					return true;
				}
				if (this.GetFlagSet(524288))
				{
					return false;
				}
				try
				{
					object[] values = this.mergedPd.GetValues(this.objs);
					for (int i = 0; i < values.Length; i++)
					{
						if (values[i] == null)
						{
							flag = false;
							break;
						}
					}
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (set) Token: 0x060054E8 RID: 21736 RVA: 0x00163DC8 File Offset: 0x00161FC8
		public override object PropertyValue
		{
			set
			{
				base.PropertyValue = value;
				base.RecreateChildren();
				if (this.Expanded)
				{
					this.GridEntryHost.Refresh(false);
				}
			}
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0015FFB8 File Offset: 0x0015E1B8
		protected override bool CreateChildren()
		{
			return this.CreateChildren(false);
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x00163DEC File Offset: 0x00161FEC
		protected override bool CreateChildren(bool diffOldChildren)
		{
			bool result;
			try
			{
				if (this.mergedPd.PropertyType.IsValueType || (this.Flags & 512) != 0)
				{
					result = base.CreateChildren(diffOldChildren);
				}
				else
				{
					base.ChildCollection.Clear();
					MultiPropertyDescriptorGridEntry[] mergedProperties = MultiSelectRootGridEntry.PropertyMerger.GetMergedProperties(this.mergedPd.GetValues(this.objs), this, this.PropertySort, this.CurrentTab);
					if (mergedProperties != null)
					{
						GridEntryCollection childCollection = base.ChildCollection;
						GridEntry[] value = mergedProperties;
						childCollection.AddRange(value);
					}
					bool flag = this.Children.Count > 0;
					if (!flag)
					{
						this.SetFlag(524288, true);
					}
					result = flag;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x00163E9C File Offset: 0x0016209C
		public override object GetChildValueOwner(GridEntry childEntry)
		{
			if (this.mergedPd.PropertyType.IsValueType || (this.Flags & 512) != 0)
			{
				return base.GetChildValueOwner(childEntry);
			}
			return this.mergedPd.GetValues(this.objs);
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x00163ED8 File Offset: 0x001620D8
		public override IComponent[] GetComponents()
		{
			IComponent[] array = new IComponent[this.objs.Length];
			Array.Copy(this.objs, 0, array, 0, this.objs.Length);
			return array;
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x00163F0C File Offset: 0x0016210C
		public override string GetPropertyTextValue(object value)
		{
			bool flag = true;
			try
			{
				if (value == null && this.mergedPd.GetValue(this.objs, out flag) == null && !flag)
				{
					return "";
				}
			}
			catch
			{
				return "";
			}
			return base.GetPropertyTextValue(value);
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00163F64 File Offset: 0x00162164
		internal override bool NotifyChildValue(GridEntry pe, int type)
		{
			bool result = false;
			IDesignerHost designerHost = this.DesignerHost;
			DesignerTransaction designerTransaction = null;
			if (designerHost != null)
			{
				designerTransaction = designerHost.CreateTransaction();
			}
			try
			{
				result = base.NotifyChildValue(pe, type);
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			return result;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00163FB0 File Offset: 0x001621B0
		protected override void NotifyParentChange(GridEntry ge)
		{
			while (ge != null && ge is PropertyDescriptorGridEntry && ((PropertyDescriptorGridEntry)ge).propertyInfo.Attributes.Contains(NotifyParentPropertyAttribute.Yes))
			{
				object valueOwner = ge.GetValueOwner();
				while (!(ge is PropertyDescriptorGridEntry) || this.OwnersEqual(valueOwner, ge.GetValueOwner()))
				{
					ge = ge.ParentGridEntry;
					if (ge == null)
					{
						break;
					}
				}
				if (ge != null)
				{
					valueOwner = ge.GetValueOwner();
					IComponentChangeService componentChangeService = this.ComponentChangeService;
					if (componentChangeService != null)
					{
						Array array = valueOwner as Array;
						if (array != null)
						{
							for (int i = 0; i < array.Length; i++)
							{
								PropertyDescriptor propertyDescriptor = ((PropertyDescriptorGridEntry)ge).propertyInfo;
								if (propertyDescriptor is MergePropertyDescriptor)
								{
									propertyDescriptor = ((MergePropertyDescriptor)propertyDescriptor)[i];
								}
								if (propertyDescriptor != null)
								{
									componentChangeService.OnComponentChanging(array.GetValue(i), propertyDescriptor);
									componentChangeService.OnComponentChanged(array.GetValue(i), propertyDescriptor, null, null);
								}
							}
						}
						else
						{
							componentChangeService.OnComponentChanging(valueOwner, ((PropertyDescriptorGridEntry)ge).propertyInfo);
							componentChangeService.OnComponentChanged(valueOwner, ((PropertyDescriptorGridEntry)ge).propertyInfo, null, null);
						}
					}
				}
			}
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x001640C0 File Offset: 0x001622C0
		internal override bool NotifyValueGivenParent(object obj, int type)
		{
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.propertyInfo);
			}
			switch (type)
			{
			case 1:
			{
				object[] array = (object[])obj;
				if (array != null && array.Length != 0)
				{
					IDesignerHost designerHost = this.DesignerHost;
					DesignerTransaction designerTransaction = null;
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("PropertyGridResetValue", new object[]
						{
							this.PropertyName
						}));
					}
					try
					{
						bool flag = !(array[0] is IComponent) || ((IComponent)array[0]).Site == null;
						if (flag && !this.OnComponentChanging())
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
								designerTransaction = null;
							}
							return false;
						}
						this.mergedPd.ResetValue(obj);
						if (flag)
						{
							this.OnComponentChanged();
						}
						this.NotifyParentChange(this);
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
						}
					}
					return false;
				}
				return false;
			}
			case 3:
			case 5:
			{
				MergePropertyDescriptor mergePropertyDescriptor = this.propertyInfo as MergePropertyDescriptor;
				if (mergePropertyDescriptor != null)
				{
					object[] array2 = (object[])obj;
					if (this.eventBindings == null)
					{
						this.eventBindings = (IEventBindingService)this.GetService(typeof(IEventBindingService));
					}
					if (this.eventBindings != null)
					{
						EventDescriptor @event = this.eventBindings.GetEvent(mergePropertyDescriptor[0]);
						if (@event != null)
						{
							return base.ViewEvent(obj, null, @event, true);
						}
					}
					return false;
				}
				return base.NotifyValueGivenParent(obj, type);
			}
			}
			return base.NotifyValueGivenParent(obj, type);
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00164240 File Offset: 0x00162440
		private bool OwnersEqual(object owner1, object owner2)
		{
			if (!(owner1 is Array))
			{
				return owner1 == owner2;
			}
			Array array = owner1 as Array;
			Array array2 = owner2 as Array;
			if (array != null && array2 != null && array.Length == array2.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array.GetValue(i) != array2.GetValue(i))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x001642A4 File Offset: 0x001624A4
		public override bool OnComponentChanging()
		{
			if (this.ComponentChangeService != null)
			{
				int num = this.objs.Length;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.ComponentChangeService.OnComponentChanging(this.objs[i], this.mergedPd[i]);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return false;
						}
						throw ex;
					}
				}
			}
			return true;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00164314 File Offset: 0x00162514
		public override void OnComponentChanged()
		{
			if (this.ComponentChangeService != null)
			{
				int num = this.objs.Length;
				for (int i = 0; i < num; i++)
				{
					this.ComponentChangeService.OnComponentChanged(this.objs[i], this.mergedPd[i], null, null);
				}
			}
		}

		// Token: 0x0400372F RID: 14127
		private MergePropertyDescriptor mergedPd;

		// Token: 0x04003730 RID: 14128
		private object[] objs;
	}
}
