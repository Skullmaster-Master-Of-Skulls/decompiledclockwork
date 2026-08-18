using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000460 RID: 1120
	[DataContract]
	public class ActionMessageFilter : MessageFilter
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000A9864 File Offset: 0x000A7A64
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x000A9895 File Offset: 0x000A7A95
		[DataMember(IsRequired = true)]
		internal string[] DCActions
		{
			get
			{
				string[] array = new string[this.actions.Count];
				this.actions.Keys.CopyTo(array, 0);
				return array;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000A989E File Offset: 0x000A7A9E
		public ActionMessageFilter(params string[] actions)
		{
			if (actions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actions");
			}
			this.Init(actions);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A98C0 File Offset: 0x000A7AC0
		private void Init(string[] actions)
		{
			if (actions.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ActionFilterEmptyList"), "actions"));
			}
			this.actions = new Dictionary<string, int>();
			for (int i = 0; i < actions.Length; i++)
			{
				if (!this.actions.ContainsKey(actions[i]))
				{
					this.actions.Add(actions[i], 0);
				}
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000A9928 File Offset: 0x000A7B28
		public ReadOnlyCollection<string> Actions
		{
			get
			{
				if (this.actionSet == null)
				{
					this.actionSet = new ReadOnlyCollection<string>(new List<string>(this.actions.Keys));
				}
				return this.actionSet;
			}
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A9953 File Offset: 0x000A7B53
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new ActionMessageFilterTable<FilterData>();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A995C File Offset: 0x000A7B5C
		private bool InnerMatch(Message message)
		{
			string text = message.Headers.Action;
			if (text == null)
			{
				text = string.Empty;
			}
			return this.actions.ContainsKey(text);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A998A File Offset: 0x000A7B8A
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return this.InnerMatch(message);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000A99A8 File Offset: 0x000A7BA8
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			Message message = messageBuffer.CreateMessage();
			bool result;
			try
			{
				result = this.InnerMatch(message);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x04002411 RID: 9233
		private Dictionary<string, int> actions;

		// Token: 0x04002412 RID: 9234
		private ReadOnlyCollection<string> actionSet;
	}
}
