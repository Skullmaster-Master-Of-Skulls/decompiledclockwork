using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel
{
	// Token: 0x02000177 RID: 375
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TransactionFlowAttribute : Attribute, IOperationBehavior
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00028B71 File Offset: 0x00026D71
		public TransactionFlowAttribute(TransactionFlowOption transactions)
		{
			TransactionFlowBindingElement.ValidateOption(transactions);
			this.transactions = transactions;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00028B86 File Offset: 0x00026D86
		public TransactionFlowOption Transactions
		{
			get
			{
				return this.transactions;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00028B90 File Offset: 0x00026D90
		internal static void OverrideFlow(BindingParameterCollection parameters, string action, MessageDirection direction, TransactionFlowOption option)
		{
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = TransactionFlowAttribute.EnsureDictionary(parameters);
			DirectionalAction key = new DirectionalAction(direction, action);
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] = option;
				return;
			}
			dictionary.Add(key, option);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00028BC8 File Offset: 0x00026DC8
		private static Dictionary<DirectionalAction, TransactionFlowOption> EnsureDictionary(BindingParameterCollection parameters)
		{
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = parameters.Find<Dictionary<DirectionalAction, TransactionFlowOption>>();
			if (dictionary == null)
			{
				dictionary = new Dictionary<DirectionalAction, TransactionFlowOption>();
				parameters.Add(dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00028BF0 File Offset: 0x00026DF0
		private void ApplyBehavior(OperationDescription description, BindingParameterCollection parameters)
		{
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = TransactionFlowAttribute.EnsureDictionary(parameters);
			dictionary[new DirectionalAction(description.Messages[0].Direction, description.Messages[0].Action)] = this.transactions;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00028C37 File Offset: 0x00026E37
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00028C39 File Offset: 0x00026E39
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00028C3B File Offset: 0x00026E3B
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			this.ApplyBehavior(description, parameters);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00028C58 File Offset: 0x00026E58
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}

		// Token: 0x04000BF1 RID: 3057
		private TransactionFlowOption transactions;
	}
}
