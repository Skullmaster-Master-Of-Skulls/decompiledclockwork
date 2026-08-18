using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000590 RID: 1424
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class MemberDescriptor
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x000E6F88 File Offset: 0x000E5188
		protected MemberDescriptor(string name) : this(name, null)
		{
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000E6F94 File Offset: 0x000E5194
		protected MemberDescriptor(string name, Attribute[] attributes)
		{
			this.lockCookie = new object();
			base..ctor();
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidMemberName"));
				}
				this.name = name;
				this.displayName = name;
				this.nameHash = name.GetHashCode();
				if (attributes != null)
				{
					this.attributes = attributes;
					this.attributesFiltered = false;
				}
				this.originalAttributes = this.attributes;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000E701C File Offset: 0x000E521C
		protected MemberDescriptor(MemberDescriptor descr)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = descr.Name;
			this.displayName = this.name;
			this.nameHash = this.name.GetHashCode();
			this.attributes = new Attribute[descr.Attributes.Count];
			descr.Attributes.CopyTo(this.attributes, 0);
			this.attributesFiltered = true;
			this.originalAttributes = this.attributes;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000E70A0 File Offset: 0x000E52A0
		protected MemberDescriptor(MemberDescriptor oldMemberDescriptor, Attribute[] newAttributes)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = oldMemberDescriptor.Name;
			this.displayName = oldMemberDescriptor.DisplayName;
			this.nameHash = this.name.GetHashCode();
			ArrayList arrayList = new ArrayList();
			if (oldMemberDescriptor.Attributes.Count != 0)
			{
				foreach (object value in oldMemberDescriptor.Attributes)
				{
					arrayList.Add(value);
				}
			}
			if (newAttributes != null)
			{
				foreach (Attribute value2 in newAttributes)
				{
					arrayList.Add(value2);
				}
			}
			this.attributes = new Attribute[arrayList.Count];
			arrayList.CopyTo(this.attributes, 0);
			this.attributesFiltered = false;
			this.originalAttributes = this.attributes;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000E719C File Offset: 0x000E539C
		// (set) Token: 0x060034EB RID: 13547 RVA: 0x000E71B0 File Offset: 0x000E53B0
		protected virtual Attribute[] AttributeArray
		{
			get
			{
				this.CheckAttributesValid();
				this.FilterAttributesIfNeeded();
				return this.attributes;
			}
			set
			{
				object obj = this.lockCookie;
				lock (obj)
				{
					this.attributes = value;
					this.originalAttributes = value;
					this.attributesFiltered = false;
					this.attributeCollection = null;
				}
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000E7208 File Offset: 0x000E5408
		public virtual AttributeCollection Attributes
		{
			get
			{
				this.CheckAttributesValid();
				AttributeCollection attributeCollection = this.attributeCollection;
				if (attributeCollection == null)
				{
					object obj = this.lockCookie;
					lock (obj)
					{
						attributeCollection = this.CreateAttributeCollection();
						this.attributeCollection = attributeCollection;
					}
				}
				return attributeCollection;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000E7264 File Offset: 0x000E5464
		public virtual string Category
		{
			get
			{
				if (this.category == null)
				{
					this.category = ((CategoryAttribute)this.Attributes[typeof(CategoryAttribute)]).Category;
				}
				return this.category;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000E7299 File Offset: 0x000E5499
		public virtual string Description
		{
			get
			{
				if (this.description == null)
				{
					this.description = ((DescriptionAttribute)this.Attributes[typeof(DescriptionAttribute)]).Description;
				}
				return this.description;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000E72CE File Offset: 0x000E54CE
		public virtual bool IsBrowsable
		{
			get
			{
				return ((BrowsableAttribute)this.Attributes[typeof(BrowsableAttribute)]).Browsable;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x000E72EF File Offset: 0x000E54EF
		public virtual string Name
		{
			get
			{
				if (this.name == null)
				{
					return "";
				}
				return this.name;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000E7305 File Offset: 0x000E5505
		protected virtual int NameHashCode
		{
			get
			{
				return this.nameHash;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x000E730D File Offset: 0x000E550D
		public virtual bool DesignTimeOnly
		{
			get
			{
				return DesignOnlyAttribute.Yes.Equals(this.Attributes[typeof(DesignOnlyAttribute)]);
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000E7330 File Offset: 0x000E5530
		public virtual string DisplayName
		{
			get
			{
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					return this.displayName;
				}
				return displayNameAttribute.DisplayName;
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000E7370 File Offset: 0x000E5570
		private void CheckAttributesValid()
		{
			if (this.attributesFiltered && this.metadataVersion != TypeDescriptor.MetadataVersion)
			{
				this.attributesFilled = false;
				this.attributesFiltered = false;
				this.attributeCollection = null;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000E739C File Offset: 0x000E559C
		protected virtual AttributeCollection CreateAttributeCollection()
		{
			return new AttributeCollection(this.AttributeArray);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000E73AC File Offset: 0x000E55AC
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			MemberDescriptor memberDescriptor = (MemberDescriptor)obj;
			this.FilterAttributesIfNeeded();
			memberDescriptor.FilterAttributesIfNeeded();
			if (memberDescriptor.nameHash != this.nameHash)
			{
				return false;
			}
			if (memberDescriptor.category == null != (this.category == null) || (this.category != null && !memberDescriptor.category.Equals(this.category)))
			{
				return false;
			}
			if (!LocalAppContextSwitches.MemberDescriptorEqualsReturnsFalseIfEquivalent)
			{
				if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.description.Equals(this.description)))
				{
					return false;
				}
			}
			else if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.category.Equals(this.description)))
			{
				return false;
			}
			if (memberDescriptor.attributes == null != (this.attributes == null))
			{
				return false;
			}
			bool result = true;
			if (this.attributes != null)
			{
				if (this.attributes.Length != memberDescriptor.attributes.Length)
				{
					return false;
				}
				for (int i = 0; i < this.attributes.Length; i++)
				{
					if (!this.attributes[i].Equals(memberDescriptor.attributes[i]))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000E74FC File Offset: 0x000E56FC
		protected virtual void FillAttributes(IList attributeList)
		{
			if (this.originalAttributes != null)
			{
				foreach (Attribute value in this.originalAttributes)
				{
					attributeList.Add(value);
				}
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000E7534 File Offset: 0x000E5734
		private void FilterAttributesIfNeeded()
		{
			if (!this.attributesFiltered)
			{
				IList list;
				if (!this.attributesFilled)
				{
					list = new ArrayList();
					try
					{
						this.FillAttributes(list);
						goto IL_34;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						goto IL_34;
					}
				}
				list = new ArrayList(this.attributes);
				IL_34:
				Hashtable hashtable = new Hashtable(list.Count);
				foreach (object obj in list)
				{
					Attribute attribute = (Attribute)obj;
					hashtable[attribute.TypeId] = attribute;
				}
				Attribute[] array = new Attribute[hashtable.Values.Count];
				hashtable.Values.CopyTo(array, 0);
				object obj2 = this.lockCookie;
				lock (obj2)
				{
					this.attributes = array;
					this.attributesFiltered = true;
					this.attributesFilled = true;
					this.metadataVersion = TypeDescriptor.MetadataVersion;
				}
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000E765C File Offset: 0x000E585C
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType)
		{
			return MemberDescriptor.FindMethod(componentClass, name, args, returnType, true);
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000E7668 File Offset: 0x000E5868
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType, bool publicOnly)
		{
			MethodInfo methodInfo;
			if (publicOnly)
			{
				methodInfo = componentClass.GetMethod(name, args);
			}
			else
			{
				methodInfo = componentClass.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
			}
			if (methodInfo != null && !methodInfo.ReturnType.IsEquivalentTo(returnType))
			{
				methodInfo = null;
			}
			return methodInfo;
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000E76AD File Offset: 0x000E58AD
		public override int GetHashCode()
		{
			return this.nameHash;
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000E76B5 File Offset: 0x000E58B5
		protected virtual object GetInvocationTarget(Type type, object instance)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.GetAssociation(type, instance);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000E76E0 File Offset: 0x000E58E0
		protected static ISite GetSite(object component)
		{
			if (!(component is IComponent))
			{
				return null;
			}
			return ((IComponent)component).Site;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000E76F7 File Offset: 0x000E58F7
		[Obsolete("This method has been deprecated. Use GetInvocationTarget instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected static object GetInvokee(Type componentClass, object component)
		{
			if (componentClass == null)
			{
				throw new ArgumentNullException("componentClass");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return TypeDescriptor.GetAssociation(componentClass, component);
		}

		// Token: 0x04002A28 RID: 10792
		private string name;

		// Token: 0x04002A29 RID: 10793
		private string displayName;

		// Token: 0x04002A2A RID: 10794
		private int nameHash;

		// Token: 0x04002A2B RID: 10795
		private AttributeCollection attributeCollection;

		// Token: 0x04002A2C RID: 10796
		private Attribute[] attributes;

		// Token: 0x04002A2D RID: 10797
		private Attribute[] originalAttributes;

		// Token: 0x04002A2E RID: 10798
		private bool attributesFiltered;

		// Token: 0x04002A2F RID: 10799
		private bool attributesFilled;

		// Token: 0x04002A30 RID: 10800
		private int metadataVersion;

		// Token: 0x04002A31 RID: 10801
		private string category;

		// Token: 0x04002A32 RID: 10802
		private string description;

		// Token: 0x04002A33 RID: 10803
		private object lockCookie;
	}
}
