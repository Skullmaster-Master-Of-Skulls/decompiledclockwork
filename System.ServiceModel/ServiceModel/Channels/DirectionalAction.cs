using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A59 RID: 2649
	internal class DirectionalAction : IComparable<DirectionalAction>
	{
		// Token: 0x060068A9 RID: 26793 RVA: 0x001870C0 File Offset: 0x001852C0
		internal DirectionalAction(MessageDirection direction, string action)
		{
			if (!MessageDirectionHelper.IsDefined(direction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("direction"));
			}
			this.direction = direction;
			if (action == null)
			{
				this.action = "*";
				this.isNullAction = true;
				return;
			}
			this.action = action;
			this.isNullAction = false;
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x060068AA RID: 26794 RVA: 0x0018711B File Offset: 0x0018531B
		public MessageDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x060068AB RID: 26795 RVA: 0x00187123 File Offset: 0x00185323
		public string Action
		{
			get
			{
				if (!this.isNullAction)
				{
					return this.action;
				}
				return null;
			}
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x00187138 File Offset: 0x00185338
		public override bool Equals(object other)
		{
			DirectionalAction directionalAction = other as DirectionalAction;
			return directionalAction != null && this.Equals(directionalAction);
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x00187158 File Offset: 0x00185358
		public bool Equals(DirectionalAction other)
		{
			return other != null && this.direction == other.direction && this.action == other.action;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x00187180 File Offset: 0x00185380
		public int CompareTo(DirectionalAction other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			if (this.direction == MessageDirection.Input && other.direction == MessageDirection.Output)
			{
				return -1;
			}
			if (this.direction == MessageDirection.Output && other.direction == MessageDirection.Input)
			{
				return 1;
			}
			return this.action.CompareTo(other.action);
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x001871D7 File Offset: 0x001853D7
		public override int GetHashCode()
		{
			return this.action.GetHashCode();
		}

		// Token: 0x04003C07 RID: 15367
		private MessageDirection direction;

		// Token: 0x04003C08 RID: 15368
		private string action;

		// Token: 0x04003C09 RID: 15369
		private bool isNullAction;
	}
}
