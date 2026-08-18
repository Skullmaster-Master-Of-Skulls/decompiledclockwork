using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D6 RID: 726
	public class MessagePartSpecification
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0005AA9F File Offset: 0x00058C9F
		public ICollection<XmlQualifiedName> HeaderTypes
		{
			get
			{
				if (this.headerTypes == null)
				{
					this.headerTypes = new List<XmlQualifiedName>();
				}
				if (this.isReadOnly)
				{
					return new ReadOnlyCollection<XmlQualifiedName>(this.headerTypes);
				}
				return this.headerTypes;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0005AACE File Offset: 0x00058CCE
		internal bool HasHeaders
		{
			get
			{
				return this.headerTypes != null && this.headerTypes.Count > 0;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0005AAE8 File Offset: 0x00058CE8
		// (set) Token: 0x060017C8 RID: 6088 RVA: 0x0005AAF0 File Offset: 0x00058CF0
		public bool IsBodyIncluded
		{
			get
			{
				return this.isBodyIncluded;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.isBodyIncluded = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0005AB1B File Offset: 0x00058D1B
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0005AB24 File Offset: 0x00058D24
		public static MessagePartSpecification NoParts
		{
			get
			{
				if (MessagePartSpecification.noParts == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.MakeReadOnly();
					MessagePartSpecification.noParts = messagePartSpecification;
				}
				return MessagePartSpecification.noParts;
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0005AB4F File Offset: 0x00058D4F
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			if (this.headerTypes != null)
			{
				this.headerTypes.Clear();
			}
			this.isBodyIncluded = false;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0005AB90 File Offset: 0x00058D90
		public void Union(MessagePartSpecification specification)
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			if (specification == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("specification");
			}
			this.isBodyIncluded |= specification.IsBodyIncluded;
			List<XmlQualifiedName> list = specification.headerTypes;
			if (list != null && list.Count > 0)
			{
				if (this.headerTypes == null)
				{
					this.headerTypes = new List<XmlQualifiedName>(list.Count);
				}
				for (int i = 0; i < list.Count; i++)
				{
					XmlQualifiedName item = list[i];
					this.headerTypes.Add(item);
				}
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0005AC38 File Offset: 0x00058E38
		public void MakeReadOnly()
		{
			if (this.isReadOnly)
			{
				return;
			}
			if (this.headerTypes != null)
			{
				List<XmlQualifiedName> list = new List<XmlQualifiedName>(this.headerTypes.Count);
				for (int i = 0; i < this.headerTypes.Count; i++)
				{
					XmlQualifiedName xmlQualifiedName = this.headerTypes[i];
					if (xmlQualifiedName != null)
					{
						bool flag = true;
						for (int j = 0; j < list.Count; j++)
						{
							XmlQualifiedName xmlQualifiedName2 = list[j];
							if (xmlQualifiedName.Name == xmlQualifiedName2.Name && xmlQualifiedName.Namespace == xmlQualifiedName2.Namespace)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							list.Add(xmlQualifiedName);
						}
					}
				}
				this.headerTypes = list;
			}
			this.isReadOnly = true;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0005ACFB File Offset: 0x00058EFB
		public MessagePartSpecification()
		{
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0005AD03 File Offset: 0x00058F03
		public MessagePartSpecification(bool isBodyIncluded)
		{
			this.isBodyIncluded = isBodyIncluded;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0005AD12 File Offset: 0x00058F12
		public MessagePartSpecification(params XmlQualifiedName[] headerTypes) : this(false, headerTypes)
		{
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005AD1C File Offset: 0x00058F1C
		public MessagePartSpecification(bool isBodyIncluded, params XmlQualifiedName[] headerTypes)
		{
			this.isBodyIncluded = isBodyIncluded;
			if (headerTypes != null && headerTypes.Length != 0)
			{
				this.headerTypes = new List<XmlQualifiedName>(headerTypes.Length);
				for (int i = 0; i < headerTypes.Length; i++)
				{
					this.headerTypes.Add(headerTypes[i]);
				}
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005AD67 File Offset: 0x00058F67
		internal bool IsHeaderIncluded(MessageHeader header)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("header");
			}
			return this.IsHeaderIncluded(header.Name, header.Namespace);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0005AD90 File Offset: 0x00058F90
		internal bool IsHeaderIncluded(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (this.headerTypes != null)
			{
				for (int i = 0; i < this.headerTypes.Count; i++)
				{
					XmlQualifiedName xmlQualifiedName = this.headerTypes[i];
					if (string.IsNullOrEmpty(xmlQualifiedName.Name))
					{
						if (xmlQualifiedName.Namespace == ns)
						{
							return true;
						}
					}
					else if (xmlQualifiedName.Name == name && xmlQualifiedName.Namespace == ns)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005AE2A File Offset: 0x0005902A
		internal bool IsEmpty()
		{
			return (this.headerTypes == null || this.headerTypes.Count <= 0) && !this.IsBodyIncluded;
		}

		// Token: 0x04001C35 RID: 7221
		private List<XmlQualifiedName> headerTypes;

		// Token: 0x04001C36 RID: 7222
		private bool isBodyIncluded;

		// Token: 0x04001C37 RID: 7223
		private bool isReadOnly;

		// Token: 0x04001C38 RID: 7224
		private static MessagePartSpecification noParts;
	}
}
