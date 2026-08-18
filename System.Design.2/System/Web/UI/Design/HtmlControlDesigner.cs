using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000044 RID: 68
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HtmlControlDesigner : ComponentDesigner
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000F615 File Offset: 0x0000D815
		public HtmlControlDesigner()
		{
			this.shouldCodeSerialize = true;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000F624 File Offset: 0x0000D824
		[Obsolete("Error: This property can no longer be referenced, and is included to support existing compiled applications. The design-time element may not always provide access to the element in the markup. There are alternate methods on WebFormsRootDesigner for handling client script and controls. http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected object DesignTimeElement
		{
			get
			{
				return this.DesignTimeElementInternal;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000F62C File Offset: 0x0000D82C
		internal object DesignTimeElementInternal
		{
			get
			{
				if (this.behavior == null)
				{
					return null;
				}
				return this.behavior.DesignTimeElement;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000F643 File Offset: 0x0000D843
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000F64B File Offset: 0x0000D84B
		[Obsolete("The recommended alternative is ControlDesigner.Tag. http://go.microsoft.com/fwlink/?linkid=14202")]
		public IHtmlControlDesignerBehavior Behavior
		{
			get
			{
				return this.BehaviorInternal;
			}
			set
			{
				this.BehaviorInternal = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000F654 File Offset: 0x0000D854
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000F65C File Offset: 0x0000D85C
		internal virtual IHtmlControlDesignerBehavior BehaviorInternal
		{
			get
			{
				return this.behavior;
			}
			set
			{
				if (this.behavior != value)
				{
					if (this.behavior != null)
					{
						this.OnBehaviorDetaching();
						this.behavior.Designer = null;
						this.behavior = null;
					}
					if (value != null)
					{
						this.behavior = value;
						this.OnBehaviorAttached();
					}
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000F698 File Offset: 0x0000D898
		public DataBindingCollection DataBindings
		{
			get
			{
				return ((IDataBindingsAccessor)base.Component).DataBindings;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000F6AA File Offset: 0x0000D8AA
		public ExpressionBindingCollection Expressions
		{
			get
			{
				return ((IExpressionsAccessor)base.Component).Expressions;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		[Obsolete("Use of this property is not recommended because code serialization is not supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual bool ShouldCodeSerialize
		{
			get
			{
				return this.ShouldCodeSerializeInternal;
			}
			set
			{
				this.ShouldCodeSerializeInternal = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000F6CD File Offset: 0x0000D8CD
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		internal virtual bool ShouldCodeSerializeInternal
		{
			get
			{
				return this.shouldCodeSerialize;
			}
			set
			{
				this.shouldCodeSerialize = value;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F6DE File Offset: 0x0000D8DE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.BehaviorInternal != null)
			{
				this.BehaviorInternal.Designer = null;
				this.BehaviorInternal = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F705 File Offset: 0x0000D905
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Control));
			base.Initialize(component);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("The recommended alternative is ControlDesigner.Tag. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void OnBehaviorAttached()
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("The recommended alternative is ControlDesigner.Tag. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void OnBehaviorDetaching()
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnSetParent()
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F720 File Offset: 0x0000D920
		protected override void PreFilterEvents(IDictionary events)
		{
			base.PreFilterEvents(events);
			if (!this.ShouldCodeSerializeInternal)
			{
				ICollection values = events.Values;
				if (values != null && values.Count != 0)
				{
					object[] array = new object[values.Count];
					values.CopyTo(array, 0);
					foreach (EventDescriptor eventDescriptor in array)
					{
						eventDescriptor = TypeDescriptor.CreateEvent(eventDescriptor.ComponentType, eventDescriptor, new Attribute[]
						{
							BrowsableAttribute.No
						});
						events[eventDescriptor.Name] = eventDescriptor;
					}
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["Modifiers"];
			if (propertyDescriptor != null)
			{
				properties["Modifiers"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					BrowsableAttribute.No
				});
			}
			properties["Expressions"] = TypeDescriptor.CreateProperty(base.GetType(), "Expressions", typeof(ExpressionBindingCollection), new Attribute[]
			{
				DesignerSerializationVisibilityAttribute.Hidden,
				CategoryAttribute.Data,
				new EditorAttribute(typeof(ExpressionsCollectionEditor), typeof(UITypeEditor)),
				new TypeConverterAttribute(typeof(ExpressionsCollectionConverter)),
				new ParenthesizePropertyNameAttribute(true),
				MergablePropertyAttribute.No,
				new DescriptionAttribute(SR.GetString("Control_Expressions"))
			});
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("The recommended alternative is to handle the Changed event on the DataBindings collection. The DataBindings collection allows more control of the databindings associated with the control. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void OnBindingsCollectionChanged(string propName)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F87D File Offset: 0x0000DA7D
		internal void OnBindingsCollectionChangedInternal(string propName)
		{
			this.OnBindingsCollectionChanged(propName);
		}

		// Token: 0x0400015F RID: 351
		private IHtmlControlDesignerBehavior behavior;

		// Token: 0x04000160 RID: 352
		private bool shouldCodeSerialize;
	}
}
