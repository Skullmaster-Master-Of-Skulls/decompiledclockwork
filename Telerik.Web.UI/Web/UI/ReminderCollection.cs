using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9B RID: 3995
	public class ReminderCollection : StronglyTypedStateManagedCollection<Reminder>, IEnumerable<Reminder>, IEnumerable
	{
		// Token: 0x06009900 RID: 39168 RVA: 0x00221FD0 File Offset: 0x002201D0
		protected override void SetDirtyObject(object o)
		{
			((Reminder)o).SetDirty();
		}

		// Token: 0x06009901 RID: 39169 RVA: 0x00222120 File Offset: 0x00220320
		IEnumerator<Reminder> IEnumerable<Reminder>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				Reminder reminder = (Reminder)obj;
				yield return reminder;
			}
			yield break;
		}

		// Token: 0x06009902 RID: 39170 RVA: 0x0022213C File Offset: 0x0022033C
		public Reminder FindByID(string id)
		{
			foreach (object obj in this)
			{
				Reminder reminder = (Reminder)obj;
				if (reminder.ID != null && reminder.ID == id)
				{
					return reminder;
				}
			}
			return null;
		}

		// Token: 0x06009903 RID: 39171 RVA: 0x002221A8 File Offset: 0x002203A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this)
			{
				Reminder reminder = (Reminder)obj;
				stringBuilder.Append(reminder.ToString());
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}
}
