using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200014B RID: 331
	[DefaultEvent("GenerateChallengeAndResponse")]
	[ToolboxBitmap(typeof(Accessor), "NoBot.bmp")]
	[Designer(typeof(NoBotExtenderDesigner))]
	public class NoBot : WebControl, INamingContainer
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x00017240 File Offset: 0x00015440
		public NoBot() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00017268 File Offset: 0x00015468
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Label label = new Label
			{
				ID = this.ID + "_NoBotLabel"
			};
			this.Controls.Add(label);
			this._extender = new NoBotExtender
			{
				ID = this.ID + "_NoBotExtender",
				TargetControlID = label.ID
			};
			this.Controls.Add(this._extender);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000172E8 File Offset: 0x000154E8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
			this.CheckResponseAndStoreState();
			NoBotEventArgs noBotEventArgs = new NoBotEventArgs();
			DateTime utcNow = DateTime.UtcNow;
			int millisecond = utcNow.Millisecond;
			noBotEventArgs.ChallengeScript = string.Format(CultureInfo.InvariantCulture, "~{0}", new object[]
			{
				millisecond.ToString(CultureInfo.InvariantCulture)
			});
			noBotEventArgs.RequiredResponse = (~millisecond).ToString(CultureInfo.InvariantCulture);
			if (this.GenerateChallengeAndResponse != null)
			{
				this.GenerateChallengeAndResponse(this, noBotEventArgs);
			}
			this._extender.ChallengeScript = noBotEventArgs.ChallengeScript;
			this._extender.ClientState = string.Empty;
			this.ViewState[this.ResponseTimeKey] = utcNow.AddSeconds((double)this._responseMinimumDelaySeconds);
			string text = this.CreateSessionKey(utcNow.Ticks);
			this.ViewState[this.SessionKeyKey] = text;
			this.Page.Session[text] = noBotEventArgs.RequiredResponse;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000173F2 File Offset: 0x000155F2
		public bool IsValid(out NoBotState state)
		{
			this.EnsureChildControls();
			this.CheckResponseAndStoreState();
			state = this._state;
			return NoBotState.Valid == state;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00017410 File Offset: 0x00015610
		public bool IsValid()
		{
			NoBotState noBotState;
			return this.IsValid(out noBotState);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00017428 File Offset: 0x00015628
		public static SortedList<DateTime, string> GetCopyOfUserAddressCache()
		{
			SortedList<DateTime, string> result;
			lock (NoBot._pastAddresses)
			{
				result = new SortedList<DateTime, string>(NoBot._pastAddresses);
			}
			return result;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00017470 File Offset: 0x00015670
		public static void EmptyUserAddressCache()
		{
			lock (NoBot._pastAddresses)
			{
				NoBot._pastAddresses.Clear();
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000174B4 File Offset: 0x000156B4
		private void CheckResponseAndStoreState()
		{
			if (NoBotState.InvalidUnknown != this._state)
			{
				return;
			}
			try
			{
				if (!this.Page.IsPostBack)
				{
					this._state = NoBotState.Valid;
				}
				else
				{
					DateTime t = (DateTime)this.ViewState[this.ResponseTimeKey];
					DateTime utcNow = DateTime.UtcNow;
					if (utcNow < t)
					{
						this._state = NoBotState.InvalidResponseTooSoon;
					}
					else
					{
						lock (NoBot._pastAddresses)
						{
							string userHostAddress = this.Page.Request.UserHostAddress;
							DateTime key = utcNow;
							while (NoBot._pastAddresses.ContainsKey(key))
							{
								key = key.AddTicks(1L);
							}
							NoBot._pastAddresses.Add(key, userHostAddress);
							DateTime t2 = utcNow.AddSeconds((double)(-(double)this._cutoffWindowSeconds));
							int num = 0;
							using (IEnumerator<DateTime> enumerator = NoBot._pastAddresses.Keys.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									DateTime t3 = enumerator.Current;
									if (!(t3 < t2))
									{
										break;
									}
									num++;
								}
								goto IL_108;
							}
							IL_F7:
							NoBot._pastAddresses.RemoveAt(0);
							num--;
							IL_108:
							if (0 < num)
							{
								goto IL_F7;
							}
							int num2 = 0;
							foreach (string b in NoBot._pastAddresses.Values)
							{
								if (userHostAddress == b)
								{
									num2++;
								}
							}
							if (this._cutoffMaximumInstances < num2)
							{
								this._state = NoBotState.InvalidAddressTooActive;
								return;
							}
						}
						string name = (string)this.ViewState[this.SessionKeyKey];
						string a = (string)this.Page.Session[name];
						this.Page.Session.Remove(name);
						if (a != this._extender.ClientState)
						{
							this._state = NoBotState.InvalidBadResponse;
						}
						else
						{
							this._state = NoBotState.Valid;
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				this._state = NoBotState.InvalidBadSession;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00017710 File Offset: 0x00015910
		private string ResponseTimeKey
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "NoBot_ResponseTimeKey_{0}", new object[]
				{
					this.UniqueID
				});
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00017740 File Offset: 0x00015940
		private string SessionKeyKey
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "NoBot_SessionKeyKey_{0}", new object[]
				{
					this.UniqueID
				});
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00017770 File Offset: 0x00015970
		private string CreateSessionKey(long ticks)
		{
			return string.Format(CultureInfo.InvariantCulture, "NoBot_SessionKey_{0}_{1}", new object[]
			{
				this.UniqueID,
				ticks
			});
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060008B5 RID: 2229 RVA: 0x000177A8 File Offset: 0x000159A8
		// (remove) Token: 0x060008B6 RID: 2230 RVA: 0x000177E0 File Offset: 0x000159E0
		public event EventHandler<NoBotEventArgs> GenerateChallengeAndResponse;

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00017815 File Offset: 0x00015A15
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001781D File Offset: 0x00015A1D
		public int ResponseMinimumDelaySeconds
		{
			get
			{
				return this._responseMinimumDelaySeconds;
			}
			set
			{
				this._responseMinimumDelaySeconds = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00017826 File Offset: 0x00015A26
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001782E File Offset: 0x00015A2E
		public int CutoffWindowSeconds
		{
			get
			{
				return this._cutoffWindowSeconds;
			}
			set
			{
				this._cutoffWindowSeconds = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00017837 File Offset: 0x00015A37
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001783F File Offset: 0x00015A3F
		public int CutoffMaximumInstances
		{
			get
			{
				return this._cutoffMaximumInstances;
			}
			set
			{
				this._cutoffMaximumInstances = value;
			}
		}

		// Token: 0x0400036C RID: 876
		private static SortedList<DateTime, string> _pastAddresses = new SortedList<DateTime, string>();

		// Token: 0x0400036D RID: 877
		private int _responseMinimumDelaySeconds = 2;

		// Token: 0x0400036E RID: 878
		private int _cutoffWindowSeconds = 60;

		// Token: 0x0400036F RID: 879
		private int _cutoffMaximumInstances = 5;

		// Token: 0x04000370 RID: 880
		private NoBotExtender _extender;

		// Token: 0x04000371 RID: 881
		private NoBotState _state = NoBotState.InvalidUnknown;
	}
}
