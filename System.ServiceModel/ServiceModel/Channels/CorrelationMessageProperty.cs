using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.DurableInstancing;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B6 RID: 2230
	[DataContract]
	public class CorrelationMessageProperty
	{
		// Token: 0x06005502 RID: 21762 RVA: 0x001388E8 File Offset: 0x00136AE8
		public CorrelationMessageProperty(InstanceKey correlationKey, IEnumerable<InstanceKey> additionalKeys) : this(correlationKey, additionalKeys, null)
		{
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x001388F4 File Offset: 0x00136AF4
		public CorrelationMessageProperty(InstanceKey correlationKey, IEnumerable<InstanceKey> additionalKeys, IEnumerable<InstanceKey> transientCorrelations)
		{
			if (correlationKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("correlationKey");
			}
			if (additionalKeys == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("additionalKeys");
			}
			this.correlationKey = correlationKey;
			ICollection<InstanceKey> collection = additionalKeys as ICollection<InstanceKey>;
			if (collection != null && collection.Count == 0)
			{
				this.additionalKeys = CorrelationMessageProperty.emptyInstanceKeyList;
			}
			else
			{
				this.additionalKeys = (additionalKeys as ReadOnlyCollection<InstanceKey>);
				if (this.additionalKeys == null)
				{
					IList<InstanceKey> list = additionalKeys as IList<InstanceKey>;
					if (list == null)
					{
						list = new List<InstanceKey>(additionalKeys);
					}
					this.additionalKeys = new ReadOnlyCollection<InstanceKey>(list);
				}
			}
			ICollection<InstanceKey> collection2 = transientCorrelations as ICollection<InstanceKey>;
			if (transientCorrelations == null || (collection2 != null && collection2.Count == 0))
			{
				this.transientCorrelations = CorrelationMessageProperty.emptyInstanceKeyList;
				return;
			}
			this.transientCorrelations = (transientCorrelations as ReadOnlyCollection<InstanceKey>);
			if (this.transientCorrelations == null)
			{
				IList<InstanceKey> list2 = transientCorrelations as IList<InstanceKey>;
				if (list2 == null)
				{
					list2 = new List<InstanceKey>(transientCorrelations);
				}
				this.transientCorrelations = new ReadOnlyCollection<InstanceKey>(list2);
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x001389D6 File Offset: 0x00136BD6
		public static string Name
		{
			get
			{
				return "CorrelationMessageProperty";
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x001389DD File Offset: 0x00136BDD
		public InstanceKey CorrelationKey
		{
			get
			{
				return this.correlationKey;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x001389E5 File Offset: 0x00136BE5
		public ReadOnlyCollection<InstanceKey> AdditionalKeys
		{
			get
			{
				if (this.additionalKeys == null)
				{
					this.additionalKeys = CorrelationMessageProperty.emptyInstanceKeyList;
				}
				return this.additionalKeys;
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x00138A00 File Offset: 0x00136C00
		public ReadOnlyCollection<InstanceKey> TransientCorrelations
		{
			get
			{
				if (this.transientCorrelations == null)
				{
					this.transientCorrelations = CorrelationMessageProperty.emptyInstanceKeyList;
				}
				return this.transientCorrelations;
			}
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00138A1B File Offset: 0x00136C1B
		public static bool TryGet(Message message, out CorrelationMessageProperty property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return CorrelationMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00138A3C File Offset: 0x00136C3C
		public static bool TryGet(MessageProperties properties, out CorrelationMessageProperty property)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			object obj = null;
			if (properties.TryGetValue("CorrelationMessageProperty", out obj))
			{
				property = (obj as CorrelationMessageProperty);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x00138A7F File Offset: 0x00136C7F
		// (set) Token: 0x0600550B RID: 21771 RVA: 0x00138A87 File Offset: 0x00136C87
		[DataMember(Name = "CorrelationKey", EmitDefaultValue = false)]
		internal InstanceKey SerializedCorrelationKey
		{
			get
			{
				return this.correlationKey;
			}
			set
			{
				this.correlationKey = value;
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00138A90 File Offset: 0x00136C90
		// (set) Token: 0x0600550D RID: 21773 RVA: 0x00138AAC File Offset: 0x00136CAC
		[DataMember(Name = "AdditionalCorrelations", EmitDefaultValue = false)]
		internal List<InstanceKey> SerializedAdditionalKeys
		{
			get
			{
				if (this.AdditionalKeys.Count == 0)
				{
					return null;
				}
				return new List<InstanceKey>(this.AdditionalKeys);
			}
			set
			{
				this.additionalKeys = new ReadOnlyCollection<InstanceKey>(value);
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x00138ABA File Offset: 0x00136CBA
		// (set) Token: 0x0600550F RID: 21775 RVA: 0x00138AD6 File Offset: 0x00136CD6
		[DataMember(Name = "TransientCorrelations", EmitDefaultValue = false)]
		internal List<InstanceKey> SerializedTransientCorrelations
		{
			get
			{
				if (this.TransientCorrelations.Count == 0)
				{
					return null;
				}
				return new List<InstanceKey>(this.TransientCorrelations);
			}
			set
			{
				this.transientCorrelations = new ReadOnlyCollection<InstanceKey>(value);
			}
		}

		// Token: 0x04003353 RID: 13139
		private static readonly ReadOnlyCollection<InstanceKey> emptyInstanceKeyList = new ReadOnlyCollection<InstanceKey>(new List<InstanceKey>(0));

		// Token: 0x04003354 RID: 13140
		private const string PropertyName = "CorrelationMessageProperty";

		// Token: 0x04003355 RID: 13141
		private ReadOnlyCollection<InstanceKey> additionalKeys;

		// Token: 0x04003356 RID: 13142
		private InstanceKey correlationKey;

		// Token: 0x04003357 RID: 13143
		private ReadOnlyCollection<InstanceKey> transientCorrelations;
	}
}
