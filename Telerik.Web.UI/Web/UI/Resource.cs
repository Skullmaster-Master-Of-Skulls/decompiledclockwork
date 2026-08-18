using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012EF RID: 4847
	public class Resource : StateManager, IEquatable<Resource>, ICustomTypeDescriptor
	{
		// Token: 0x170041A5 RID: 16805
		// (get) Token: 0x0600CB73 RID: 52083 RVA: 0x002D7889 File Offset: 0x002D5A89
		// (set) Token: 0x0600CB74 RID: 52084 RVA: 0x002D789B File Offset: 0x002D5A9B
		public object Key
		{
			get
			{
				return base.ViewState["Key"];
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x170041A6 RID: 16806
		// (get) Token: 0x0600CB75 RID: 52085 RVA: 0x002D78AE File Offset: 0x002D5AAE
		// (set) Token: 0x0600CB76 RID: 52086 RVA: 0x002D78CE File Offset: 0x002D5ACE
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170041A7 RID: 16807
		// (get) Token: 0x0600CB77 RID: 52087 RVA: 0x002D78E1 File Offset: 0x002D5AE1
		// (set) Token: 0x0600CB78 RID: 52088 RVA: 0x002D7901 File Offset: 0x002D5B01
		public string Type
		{
			get
			{
				return (string)(base.ViewState["Type"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x170041A8 RID: 16808
		// (get) Token: 0x0600CB79 RID: 52089 RVA: 0x002D7914 File Offset: 0x002D5B14
		// (set) Token: 0x0600CB7A RID: 52090 RVA: 0x002D7935 File Offset: 0x002D5B35
		public bool Available
		{
			get
			{
				return (bool)(base.ViewState["Available"] ?? true);
			}
			set
			{
				base.ViewState["Available"] = value;
			}
		}

		// Token: 0x170041A9 RID: 16809
		// (get) Token: 0x0600CB7B RID: 52091 RVA: 0x002D794D File Offset: 0x002D5B4D
		// (set) Token: 0x0600CB7C RID: 52092 RVA: 0x002D796D File Offset: 0x002D5B6D
		public string CssClass
		{
			get
			{
				return (string)(base.ViewState["CssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x170041AA RID: 16810
		// (get) Token: 0x0600CB7D RID: 52093 RVA: 0x002D7980 File Offset: 0x002D5B80
		[ScriptIgnore]
		[Browsable(false)]
		[NonSerializedInControlState]
		public System.Web.UI.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new System.Web.UI.AttributeCollection(this.AttributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x170041AB RID: 16811
		// (get) Token: 0x0600CB7E RID: 52094 RVA: 0x002D79A1 File Offset: 0x002D5BA1
		[ScriptIgnore]
		[Browsable(false)]
		[NonSerializedInControlState]
		public IList<SchedulerResourceContainer> HeaderControls
		{
			get
			{
				return this._resourceControls;
			}
		}

		// Token: 0x170041AC RID: 16812
		// (get) Token: 0x0600CB7F RID: 52095 RVA: 0x002D79A9 File Offset: 0x002D5BA9
		// (set) Token: 0x0600CB80 RID: 52096 RVA: 0x002D79B1 File Offset: 0x002D5BB1
		public virtual object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x0600CB81 RID: 52097 RVA: 0x002D79BA File Offset: 0x002D5BBA
		public Resource()
		{
		}

		// Token: 0x0600CB82 RID: 52098 RVA: 0x002D79CD File Offset: 0x002D5BCD
		public Resource(string resType, object resKey, string resText)
		{
			this.Type = resType;
			this.Key = resKey;
			this.Text = resText;
		}

		// Token: 0x170041AD RID: 16813
		// (get) Token: 0x0600CB83 RID: 52099 RVA: 0x002D79F5 File Offset: 0x002D5BF5
		private StateBag AttributeState
		{
			get
			{
				if (this._attributeState == null)
				{
					this._attributeState = new StateBag();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._attributeState).TrackViewState();
					}
				}
				return this._attributeState;
			}
		}

		// Token: 0x0600CB84 RID: 52100 RVA: 0x002D7A23 File Offset: 0x002D5C23
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AttributeState.SetDirty(true);
		}

		// Token: 0x0600CB85 RID: 52101 RVA: 0x002D7A38 File Offset: 0x002D5C38
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.AttributeState).LoadViewState(array[1]);
		}

		// Token: 0x0600CB86 RID: 52102 RVA: 0x002D7A64 File Offset: 0x002D5C64
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.AttributeState).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600CB87 RID: 52103 RVA: 0x002D7A9C File Offset: 0x002D5C9C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x0600CB88 RID: 52104 RVA: 0x002D7AB8 File Offset: 0x002D5CB8
		public override bool Equals(object obj)
		{
			Resource resource = obj as Resource;
			return !(resource == null) && this.Equals(resource);
		}

		// Token: 0x0600CB89 RID: 52105 RVA: 0x002D7AE0 File Offset: 0x002D5CE0
		public bool Equals(Resource res)
		{
			return !(res == null) && (this.Text == res.Text && this.Key.Equals(res.Key)) && this.Type == res.Type;
		}

		// Token: 0x0600CB8A RID: 52106 RVA: 0x002D7B31 File Offset: 0x002D5D31
		public static bool operator ==(Resource o1, Resource o2)
		{
			if (o1 != null)
			{
				return o1.Equals(o2);
			}
			return o2 == null;
		}

		// Token: 0x0600CB8B RID: 52107 RVA: 0x002D7B42 File Offset: 0x002D5D42
		public static bool operator !=(Resource o1, Resource o2)
		{
			if (o1 != null)
			{
				return !o1.Equals(o2);
			}
			return o2 != null;
		}

		// Token: 0x0600CB8C RID: 52108 RVA: 0x002D7B59 File Offset: 0x002D5D59
		public override int GetHashCode()
		{
			return this.Text.GetHashCode() ^ this.Key.GetHashCode() ^ this.Type.GetHashCode();
		}

		// Token: 0x0600CB8D RID: 52109 RVA: 0x002D7B7E File Offset: 0x002D5D7E
		System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600CB8E RID: 52110 RVA: 0x002D7B87 File Offset: 0x002D5D87
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600CB8F RID: 52111 RVA: 0x002D7B90 File Offset: 0x002D5D90
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x0600CB90 RID: 52112 RVA: 0x002D7B99 File Offset: 0x002D5D99
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600CB91 RID: 52113 RVA: 0x002D7BA2 File Offset: 0x002D5DA2
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600CB92 RID: 52114 RVA: 0x002D7BAB File Offset: 0x002D5DAB
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600CB93 RID: 52115 RVA: 0x002D7BB4 File Offset: 0x002D5DB4
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600CB94 RID: 52116 RVA: 0x002D7BBE File Offset: 0x002D5DBE
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600CB95 RID: 52117 RVA: 0x002D7BCB File Offset: 0x002D5DCB
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600CB96 RID: 52118 RVA: 0x002D7BD8 File Offset: 0x002D5DD8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return this.MapAttributesToProperties(properties);
		}

		// Token: 0x0600CB97 RID: 52119 RVA: 0x002D7BF8 File Offset: 0x002D5DF8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return this.MapAttributesToProperties(properties);
		}

		// Token: 0x0600CB98 RID: 52120 RVA: 0x002D7C14 File Offset: 0x002D5E14
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0600CB99 RID: 52121 RVA: 0x002D7C18 File Offset: 0x002D5E18
		private PropertyDescriptorCollection MapAttributesToProperties(PropertyDescriptorCollection originalProperties)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			foreach (object obj in originalProperties)
			{
				PropertyDescriptor value = (PropertyDescriptor)obj;
				propertyDescriptorCollection.Add(value);
			}
			foreach (object obj2 in this.Attributes.Keys)
			{
				string propertyName = (string)obj2;
				propertyDescriptorCollection.Add(new ResourceAttributePropertyDescriptor(propertyName));
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x04003577 RID: 13687
		private System.Web.UI.AttributeCollection _attributes;

		// Token: 0x04003578 RID: 13688
		private StateBag _attributeState;

		// Token: 0x04003579 RID: 13689
		private readonly IList<SchedulerResourceContainer> _resourceControls = new List<SchedulerResourceContainer>();

		// Token: 0x0400357A RID: 13690
		private object _dataItem;
	}
}
