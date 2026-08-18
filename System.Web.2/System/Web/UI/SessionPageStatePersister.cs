using System;
using System.Collections;
using System.Web.Configuration;
using System.Web.Security.Cryptography;
using System.Web.SessionState;

namespace System.Web.UI
{
	// Token: 0x020002F7 RID: 759
	public class SessionPageStatePersister : PageStatePersister
	{
		// Token: 0x06002322 RID: 8994 RVA: 0x0007250C File Offset: 0x0007070C
		public SessionPageStatePersister(Page page) : base(page)
		{
			HttpSessionState httpSessionState = null;
			try
			{
				httpSessionState = page.Session;
			}
			catch
			{
			}
			if (httpSessionState == null)
			{
				throw new ArgumentException(SR.GetString("SessionPageStatePersister_SessionMustBeEnabled"));
			}
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00072554 File Offset: 0x00070754
		public override void Load()
		{
			if (base.Page.RequestValueCollection == null)
			{
				return;
			}
			try
			{
				string requestViewStateString = base.Page.RequestViewStateString;
				string text = null;
				bool flag = false;
				if (!string.IsNullOrEmpty(requestViewStateString))
				{
					Pair pair = (Pair)Util.DeserializeWithAssert(base.StateFormatter2, requestViewStateString, Purpose.WebForms_SessionPageStatePersister_ClientState);
					if ((bool)pair.First)
					{
						text = (string)pair.Second;
						flag = true;
					}
					else
					{
						Pair pair2 = (Pair)pair.Second;
						text = (string)pair2.First;
						base.ControlState = pair2.Second;
					}
				}
				if (text != null)
				{
					object obj = base.Page.Session["__SESSIONVIEWSTATE" + text];
					if (flag)
					{
						Pair pair3 = obj as Pair;
						if (pair3 != null)
						{
							base.ViewState = pair3.First;
							base.ControlState = pair3.Second;
						}
					}
					else
					{
						base.ViewState = obj;
					}
				}
			}
			catch (Exception innerException)
			{
				HttpException ex = new HttpException(SR.GetString("Invalid_ControlState"), innerException);
				ex.SetFormatter(new UseLastUnhandledErrorFormatter(ex));
				throw ex;
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00072678 File Offset: 0x00070878
		public override void Save()
		{
			bool flag = false;
			object obj = null;
			Triplet triplet = base.ViewState as Triplet;
			if (base.ControlState != null || ((triplet == null || triplet.Second != null || triplet.Third != null) && base.ViewState != null))
			{
				HttpSessionState session = base.Page.Session;
				string text = Convert.ToString(DateTime.Now.Ticks, 16);
				flag = base.Page.Request.Browser.RequiresControlStateInSession;
				object value;
				if (flag)
				{
					value = new Pair(base.ViewState, base.ControlState);
					obj = text;
				}
				else
				{
					value = base.ViewState;
					obj = new Pair(text, base.ControlState);
				}
				string text2 = "__SESSIONVIEWSTATE" + text;
				session[text2] = value;
				Queue queue = session["__VIEWSTATEQUEUE"] as Queue;
				if (queue == null)
				{
					queue = new Queue();
					session["__VIEWSTATEQUEUE"] = queue;
				}
				queue.Enqueue(text2);
				SessionPageStateSection sessionPageState = RuntimeConfig.GetConfig(base.Page.Request.Context).SessionPageState;
				int count = queue.Count;
				if ((sessionPageState != null && count > sessionPageState.HistorySize) || (sessionPageState == null && count > 9))
				{
					string name = (string)queue.Dequeue();
					session.Remove(name);
				}
			}
			if (obj != null)
			{
				base.Page.ClientState = Util.SerializeWithAssert(base.StateFormatter2, new Pair(flag, obj), Purpose.WebForms_SessionPageStatePersister_ClientState);
			}
		}

		// Token: 0x04001CA0 RID: 7328
		private const string _viewStateSessionKey = "__SESSIONVIEWSTATE";

		// Token: 0x04001CA1 RID: 7329
		private const string _viewStateQueueKey = "__VIEWSTATEQUEUE";
	}
}
