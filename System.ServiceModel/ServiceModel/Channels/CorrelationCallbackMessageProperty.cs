using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B1 RID: 2225
	public abstract class CorrelationCallbackMessageProperty : IMessageProperty
	{
		// Token: 0x060054CC RID: 21708 RVA: 0x00138032 File Offset: 0x00136232
		protected CorrelationCallbackMessageProperty(ICollection<string> neededData)
		{
			if (neededData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("neededData");
			}
			if (neededData.Count > 0)
			{
				this.neededData = new List<string>(neededData);
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00138064 File Offset: 0x00136264
		protected CorrelationCallbackMessageProperty(CorrelationCallbackMessageProperty callback)
		{
			if (callback == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callback");
			}
			if (callback.data != null)
			{
				this.data = (CorrelationDataMessageProperty)callback.data.CreateCopy();
			}
			if (callback.neededData != null && callback.neededData.Count > 0)
			{
				this.neededData = new List<string>(callback.neededData);
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060054CE RID: 21710 RVA: 0x001380CF File Offset: 0x001362CF
		public static string Name
		{
			get
			{
				return "CorrelationCallbackMessageProperty";
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x001380D6 File Offset: 0x001362D6
		public bool IsFullyDefined
		{
			get
			{
				return this.neededData == null || this.neededData.Count == 0;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x001380F0 File Offset: 0x001362F0
		public IEnumerable<string> NeededData
		{
			get
			{
				if (this.neededData == null)
				{
					return CorrelationCallbackMessageProperty.emptyNeededData;
				}
				return this.neededData;
			}
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x00138106 File Offset: 0x00136306
		public static bool TryGet(Message message, out CorrelationCallbackMessageProperty property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return CorrelationCallbackMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00138128 File Offset: 0x00136328
		public static bool TryGet(MessageProperties properties, out CorrelationCallbackMessageProperty property)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			object obj = null;
			if (properties.TryGetValue("CorrelationCallbackMessageProperty", out obj))
			{
				property = (obj as CorrelationCallbackMessageProperty);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0013816C File Offset: 0x0013636C
		public void AddData(string name, Func<string> value)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (this.data == null)
			{
				this.data = new CorrelationDataMessageProperty();
			}
			this.data.Add(name, value);
			if (this.neededData != null)
			{
				this.neededData.Remove(name);
			}
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x001381D4 File Offset: 0x001363D4
		public IAsyncResult BeginFinalizeCorrelation(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			if (this.data != null && !message.Properties.ContainsKey(CorrelationDataMessageProperty.Name))
			{
				message.Properties[CorrelationDataMessageProperty.Name] = this.data;
			}
			return this.OnBeginFinalizeCorrelation(message, timeout, callback, state);
		}

		// Token: 0x060054D5 RID: 21717
		public abstract IMessageProperty CreateCopy();

		// Token: 0x060054D6 RID: 21718 RVA: 0x0013825B File Offset: 0x0013645B
		public Message EndFinalizeCorrelation(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			return this.OnEndFinalizeCorrelation(result);
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x00138278 File Offset: 0x00136478
		public Message FinalizeCorrelation(Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			if (this.data != null && !message.Properties.ContainsKey(CorrelationDataMessageProperty.Name))
			{
				message.Properties[CorrelationDataMessageProperty.Name] = this.data;
			}
			return this.OnFinalizeCorrelation(message, timeout);
		}

		// Token: 0x060054D8 RID: 21720
		protected abstract IAsyncResult OnBeginFinalizeCorrelation(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060054D9 RID: 21721
		protected abstract Message OnEndFinalizeCorrelation(IAsyncResult result);

		// Token: 0x060054DA RID: 21722
		protected abstract Message OnFinalizeCorrelation(Message message, TimeSpan timeout);

		// Token: 0x04003345 RID: 13125
		private const string PropertyName = "CorrelationCallbackMessageProperty";

		// Token: 0x04003346 RID: 13126
		private CorrelationDataMessageProperty data;

		// Token: 0x04003347 RID: 13127
		private List<string> neededData;

		// Token: 0x04003348 RID: 13128
		private static ReadOnlyCollection<string> emptyNeededData = new ReadOnlyCollection<string>(new List<string>(0));
	}
}
