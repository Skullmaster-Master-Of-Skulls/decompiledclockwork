using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000515 RID: 1301
	internal class SingleSelectRootGridEntry : GridEntry, IRootGridEntry
	{
		// Token: 0x06005529 RID: 21801 RVA: 0x001658A8 File Offset: 0x00163AA8
		internal SingleSelectRootGridEntry(PropertyGridView gridEntryHost, object value, GridEntry parent, IServiceProvider baseProvider, IDesignerHost host, PropertyTab tab, PropertySort sortType) : base(gridEntryHost.OwnerGrid, parent)
		{
			this.host = host;
			this.gridEntryHost = gridEntryHost;
			this.baseProvider = baseProvider;
			this.tab = tab;
			this.objValue = value;
			this.objValueClassName = TypeDescriptor.GetClassName(this.objValue);
			this.IsExpandable = true;
			this.PropertySort = sortType;
			this.InternalExpanded = true;
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x0016590F File Offset: 0x00163B0F
		internal SingleSelectRootGridEntry(PropertyGridView view, object value, IServiceProvider baseProvider, IDesignerHost host, PropertyTab tab, PropertySort sortType) : this(view, value, null, baseProvider, host, tab, sortType)
		{
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x00165921 File Offset: 0x00163B21
		// (set) Token: 0x0600552C RID: 21804 RVA: 0x0016594C File Offset: 0x00163B4C
		public override AttributeCollection BrowsableAttributes
		{
			get
			{
				if (this.browsableAttributes == null)
				{
					this.browsableAttributes = new AttributeCollection(new Attribute[]
					{
						BrowsableAttribute.Yes
					});
				}
				return this.browsableAttributes;
			}
			set
			{
				if (value == null)
				{
					this.ResetBrowsableAttributes();
					return;
				}
				bool flag = true;
				if (this.browsableAttributes != null && value != null && this.browsableAttributes.Count == value.Count)
				{
					Attribute[] array = new Attribute[this.browsableAttributes.Count];
					Attribute[] array2 = new Attribute[value.Count];
					this.browsableAttributes.CopyTo(array, 0);
					value.CopyTo(array2, 0);
					Array.Sort(array, GridEntry.AttributeTypeSorter);
					Array.Sort(array2, GridEntry.AttributeTypeSorter);
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].Equals(array2[i]))
						{
							flag = false;
							break;
						}
					}
				}
				else
				{
					flag = false;
				}
				this.browsableAttributes = value;
				if (!flag && this.Children != null && this.Children.Count > 0)
				{
					this.DisposeChildren();
				}
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00165A14 File Offset: 0x00163C14
		protected override IComponentChangeService ComponentChangeService
		{
			get
			{
				if (this.changeService == null)
				{
					this.changeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				}
				return this.changeService;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600552E RID: 21806 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool AlwaysAllowExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x00165A3F File Offset: 0x00163C3F
		// (set) Token: 0x06005530 RID: 21808 RVA: 0x00165A47 File Offset: 0x00163C47
		public override PropertyTab CurrentTab
		{
			get
			{
				return this.tab;
			}
			set
			{
				this.tab = value;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x00165A50 File Offset: 0x00163C50
		// (set) Token: 0x06005532 RID: 21810 RVA: 0x00165A58 File Offset: 0x00163C58
		internal override GridEntry DefaultChild
		{
			get
			{
				return this.propDefault;
			}
			set
			{
				this.propDefault = value;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x00165A61 File Offset: 0x00163C61
		// (set) Token: 0x06005534 RID: 21812 RVA: 0x00165A69 File Offset: 0x00163C69
		internal override IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06005535 RID: 21813 RVA: 0x00165A74 File Offset: 0x00163C74
		internal override bool ForceReadOnly
		{
			get
			{
				if (!this.forceReadOnlyChecked)
				{
					ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetAttributes(this.objValue)[typeof(ReadOnlyAttribute)];
					if ((readOnlyAttribute != null && !readOnlyAttribute.IsDefaultAttribute()) || TypeDescriptor.GetAttributes(this.objValue).Contains(InheritanceAttribute.InheritedReadOnly))
					{
						this.flags |= 1024;
					}
					this.forceReadOnlyChecked = true;
				}
				return base.ForceReadOnly || (this.GridEntryHost != null && !this.GridEntryHost.Enabled);
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x00165B06 File Offset: 0x00163D06
		// (set) Token: 0x06005537 RID: 21815 RVA: 0x00165B0E File Offset: 0x00163D0E
		internal override PropertyGridView GridEntryHost
		{
			get
			{
				return this.gridEntryHost;
			}
			set
			{
				this.gridEntryHost = value;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x00023D73 File Offset: 0x00021F73
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Root;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06005539 RID: 21817 RVA: 0x00165B18 File Offset: 0x00163D18
		public override string HelpKeyword
		{
			get
			{
				HelpKeywordAttribute helpKeywordAttribute = (HelpKeywordAttribute)TypeDescriptor.GetAttributes(this.objValue)[typeof(HelpKeywordAttribute)];
				if (helpKeywordAttribute != null && !helpKeywordAttribute.IsDefaultAttribute())
				{
					return helpKeywordAttribute.HelpKeyword;
				}
				return this.objValueClassName;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x00165B60 File Offset: 0x00163D60
		public override string PropertyLabel
		{
			get
			{
				if (this.objValue is IComponent)
				{
					ISite site = ((IComponent)this.objValue).Site;
					if (site == null)
					{
						return this.objValue.GetType().Name;
					}
					return site.Name;
				}
				else
				{
					if (this.objValue != null)
					{
						return this.objValue.ToString();
					}
					return null;
				}
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x00165BBB File Offset: 0x00163DBB
		// (set) Token: 0x0600553C RID: 21820 RVA: 0x00165BC4 File Offset: 0x00163DC4
		public override object PropertyValue
		{
			get
			{
				return this.objValue;
			}
			set
			{
				object oldObject = this.objValue;
				this.objValue = value;
				this.objValueClassName = TypeDescriptor.GetClassName(this.objValue);
				this.ownerGrid.ReplaceSelectedObject(oldObject, value);
			}
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00165C00 File Offset: 0x00163E00
		protected override bool CreateChildren()
		{
			bool result = base.CreateChildren();
			this.CategorizePropEntries();
			return result;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00165C1C File Offset: 0x00163E1C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.host = null;
				this.baseProvider = null;
				this.tab = null;
				this.gridEntryHost = null;
				this.changeService = null;
			}
			this.objValue = null;
			this.objValueClassName = null;
			this.propDefault = null;
			base.Dispose(disposing);
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x00165C6C File Offset: 0x00163E6C
		public override object GetService(Type serviceType)
		{
			object obj = null;
			if (this.host != null)
			{
				obj = this.host.GetService(serviceType);
			}
			if (obj == null && this.baseProvider != null)
			{
				obj = this.baseProvider.GetService(serviceType);
			}
			return obj;
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00165CA9 File Offset: 0x00163EA9
		public void ResetBrowsableAttributes()
		{
			this.browsableAttributes = new AttributeCollection(new Attribute[]
			{
				BrowsableAttribute.Yes
			});
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00165CC4 File Offset: 0x00163EC4
		public virtual void ShowCategories(bool fCategories)
		{
			if ((this.PropertySort &= PropertySort.Categorized) > PropertySort.NoSort != fCategories)
			{
				if (fCategories)
				{
					this.PropertySort |= PropertySort.Categorized;
				}
				else
				{
					this.PropertySort &= (PropertySort)(-3);
				}
				if (this.Expandable && base.ChildCollection != null)
				{
					this.CreateChildren();
				}
			}
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00165D24 File Offset: 0x00163F24
		internal void CategorizePropEntries()
		{
			if (this.Children.Count > 0)
			{
				GridEntry[] array = new GridEntry[this.Children.Count];
				this.Children.CopyTo(array, 0);
				if ((this.PropertySort & PropertySort.Categorized) != PropertySort.NoSort)
				{
					Hashtable hashtable = new Hashtable();
					foreach (GridEntry gridEntry in array)
					{
						if (gridEntry != null)
						{
							string propertyCategory = gridEntry.PropertyCategory;
							ArrayList arrayList = (ArrayList)hashtable[propertyCategory];
							if (arrayList == null)
							{
								arrayList = new ArrayList();
								hashtable[propertyCategory] = arrayList;
							}
							arrayList.Add(gridEntry);
						}
					}
					ArrayList arrayList2 = new ArrayList();
					IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ArrayList arrayList3 = (ArrayList)enumerator.Value;
						if (arrayList3 != null)
						{
							string name = (string)enumerator.Key;
							if (arrayList3.Count > 0)
							{
								GridEntry[] array2 = new GridEntry[arrayList3.Count];
								arrayList3.CopyTo(array2, 0);
								try
								{
									arrayList2.Add(new CategoryGridEntry(this.ownerGrid, this, name, array2));
								}
								catch
								{
								}
							}
						}
					}
					array = new GridEntry[arrayList2.Count];
					arrayList2.CopyTo(array, 0);
					object[] items = array;
					StringSorter.Sort(items);
					base.ChildCollection.Clear();
					base.ChildCollection.AddRange(array);
				}
			}
		}

		// Token: 0x04003751 RID: 14161
		protected object objValue;

		// Token: 0x04003752 RID: 14162
		protected string objValueClassName;

		// Token: 0x04003753 RID: 14163
		protected GridEntry propDefault;

		// Token: 0x04003754 RID: 14164
		protected IDesignerHost host;

		// Token: 0x04003755 RID: 14165
		protected IServiceProvider baseProvider;

		// Token: 0x04003756 RID: 14166
		protected PropertyTab tab;

		// Token: 0x04003757 RID: 14167
		protected PropertyGridView gridEntryHost;

		// Token: 0x04003758 RID: 14168
		protected AttributeCollection browsableAttributes;

		// Token: 0x04003759 RID: 14169
		private IComponentChangeService changeService;

		// Token: 0x0400375A RID: 14170
		protected bool forceReadOnlyChecked;
	}
}
