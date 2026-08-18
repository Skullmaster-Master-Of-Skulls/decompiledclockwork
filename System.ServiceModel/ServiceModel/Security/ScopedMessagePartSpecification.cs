using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F5 RID: 757
	public class ScopedMessagePartSpecification
	{
		// Token: 0x0600196E RID: 6510 RVA: 0x0005ED68 File Offset: 0x0005CF68
		public ScopedMessagePartSpecification()
		{
			this.channelParts = new MessagePartSpecification();
			this.actionParts = new Dictionary<string, MessagePartSpecification>();
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005ED86 File Offset: 0x0005CF86
		public ICollection<string> Actions
		{
			get
			{
				return this.actionParts.Keys;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0005ED93 File Offset: 0x0005CF93
		public MessagePartSpecification ChannelParts
		{
			get
			{
				return this.channelParts;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0005ED9B File Offset: 0x0005CF9B
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0005EDA4 File Offset: 0x0005CFA4
		public ScopedMessagePartSpecification(ScopedMessagePartSpecification other) : this()
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("other"));
			}
			this.channelParts.Union(other.channelParts);
			if (other.actionParts != null)
			{
				foreach (string key in other.actionParts.Keys)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.Union(other.actionParts[key]);
					this.actionParts[key] = messagePartSpecification;
				}
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0005EE54 File Offset: 0x0005D054
		internal ScopedMessagePartSpecification(ScopedMessagePartSpecification other, bool newIncludeBody) : this(other)
		{
			this.channelParts.IsBodyIncluded = newIncludeBody;
			foreach (string key in this.actionParts.Keys)
			{
				this.actionParts[key].IsBodyIncluded = newIncludeBody;
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0005EECC File Offset: 0x0005D0CC
		public void AddParts(MessagePartSpecification parts)
		{
			if (parts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parts"));
			}
			this.ThrowIfReadOnly();
			this.channelParts.Union(parts);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		public void AddParts(MessagePartSpecification parts, string action)
		{
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("action"));
			}
			if (parts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parts"));
			}
			this.ThrowIfReadOnly();
			if (!this.actionParts.ContainsKey(action))
			{
				this.actionParts[action] = new MessagePartSpecification();
			}
			this.actionParts[action].Union(parts);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005EF6C File Offset: 0x0005D16C
		internal void AddParts(MessagePartSpecification parts, XmlDictionaryString action)
		{
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("action"));
			}
			this.AddParts(parts, action.Value);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0005EF94 File Offset: 0x0005D194
		internal bool IsEmpty()
		{
			bool result;
			if (!this.channelParts.IsEmpty())
			{
				result = false;
			}
			else
			{
				result = true;
				foreach (string action in this.Actions)
				{
					MessagePartSpecification messagePartSpecification;
					if (this.TryGetParts(action, true, out messagePartSpecification) && !messagePartSpecification.IsEmpty())
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005F008 File Offset: 0x0005D208
		public bool TryGetParts(string action, bool excludeChannelScope, out MessagePartSpecification parts)
		{
			if (action == null)
			{
				action = "*";
			}
			parts = null;
			if (this.isReadOnly)
			{
				if (this.readOnlyNormalizedActionParts.ContainsKey(action))
				{
					if (excludeChannelScope)
					{
						parts = this.actionParts[action];
					}
					else
					{
						parts = this.readOnlyNormalizedActionParts[action];
					}
				}
			}
			else if (this.actionParts.ContainsKey(action))
			{
				MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
				messagePartSpecification.Union(this.actionParts[action]);
				if (!excludeChannelScope)
				{
					messagePartSpecification.Union(this.channelParts);
				}
				parts = messagePartSpecification;
			}
			return parts != null;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005F098 File Offset: 0x0005D298
		internal void CopyTo(ScopedMessagePartSpecification target)
		{
			if (target == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("target");
			}
			target.ChannelParts.IsBodyIncluded = this.ChannelParts.IsBodyIncluded;
			foreach (XmlQualifiedName xmlQualifiedName in this.ChannelParts.HeaderTypes)
			{
				if (!target.channelParts.IsHeaderIncluded(xmlQualifiedName.Name, xmlQualifiedName.Namespace))
				{
					target.ChannelParts.HeaderTypes.Add(xmlQualifiedName);
				}
			}
			foreach (string text in this.actionParts.Keys)
			{
				target.AddParts(this.actionParts[text], text);
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005F18C File Offset: 0x0005D38C
		public bool TryGetParts(string action, out MessagePartSpecification parts)
		{
			return this.TryGetParts(action, false, out parts);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005F198 File Offset: 0x0005D398
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.readOnlyNormalizedActionParts = new Dictionary<string, MessagePartSpecification>();
				foreach (string key in this.actionParts.Keys)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.Union(this.actionParts[key]);
					messagePartSpecification.Union(this.channelParts);
					messagePartSpecification.MakeReadOnly();
					this.readOnlyNormalizedActionParts[key] = messagePartSpecification;
				}
				this.isReadOnly = true;
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005F23C File Offset: 0x0005D43C
		private void ThrowIfReadOnly()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001C9B RID: 7323
		private MessagePartSpecification channelParts;

		// Token: 0x04001C9C RID: 7324
		private Dictionary<string, MessagePartSpecification> actionParts;

		// Token: 0x04001C9D RID: 7325
		private Dictionary<string, MessagePartSpecification> readOnlyNormalizedActionParts;

		// Token: 0x04001C9E RID: 7326
		private bool isReadOnly;
	}
}
