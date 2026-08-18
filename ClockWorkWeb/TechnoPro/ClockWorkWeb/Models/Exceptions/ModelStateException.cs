using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Models.Exceptions
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class ModelStateException : Exception
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0003A6E6 File Offset: 0x000388E6
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0003A6EE File Offset: 0x000388EE
		public Dictionary<string, string> Errors { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0003A6F8 File Offset: 0x000388F8
		public override string Message
		{
			get
			{
				string result;
				if (this.Errors.Count <= 0)
				{
					result = null;
				}
				else
				{
					result = string.Join(" ", (from e in this.Errors
					select e.Value).ToArray<string>());
				}
				return result;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003A754 File Offset: 0x00038954
		public ModelStateException()
		{
			this.Errors = new Dictionary<string, string>();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003A76C File Offset: 0x0003896C
		public ModelStateException(ModelStateDictionary modelState) : this()
		{
			bool flag = modelState == null;
			if (flag)
			{
				throw new ArgumentNullException("modelState");
			}
			bool flag2 = !modelState.IsValid;
			if (flag2)
			{
				foreach (KeyValuePair<string, ModelState> keyValuePair in modelState)
				{
					bool flag3 = keyValuePair.Value.Errors.Count > 0;
					if (flag3)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (ModelError modelError in keyValuePair.Value.Errors)
						{
							stringBuilder.AppendLine(modelError.ErrorMessage);
						}
						this.Errors.Add(keyValuePair.Key, stringBuilder.ToString());
					}
				}
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0003A874 File Offset: 0x00038A74
		protected ModelStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			bool flag = info == null;
			if (flag)
			{
				throw new ArgumentNullException("info");
			}
			this.Errors = (info.GetValue("ModelStateException.Errors", typeof(Dictionary<string, string>)) as Dictionary<string, string>);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0003A8C0 File Offset: 0x00038AC0
		public ModelStateException(string message) : base(message)
		{
			this.Errors = new Dictionary<string, string>();
			this.Errors.Add(string.Empty, message);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0003A8E9 File Offset: 0x00038AE9
		public ModelStateException(string message, Exception innerException) : base(message, innerException)
		{
			this.Errors = new Dictionary<string, string>();
			this.Errors.Add(string.Empty, message);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0003A914 File Offset: 0x00038B14
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			bool flag = info == null;
			if (flag)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ModelStateException.Errors", this.Errors, typeof(Dictionary<string, string>));
			base.GetObjectData(info, context);
		}
	}
}
