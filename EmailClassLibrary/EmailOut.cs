using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EmailClassLibrary
{
	// Token: 0x02000003 RID: 3
	public class EmailOut
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public static EmailResult SendEmail(EmailSoftware emailSoftware, string to, string subject, string cc, string bcc, string attachments, string body, bool bodyIsHtml, params object[] additionalParameters)
		{
			EmailResult emailResult = new EmailResult();
			if (emailSoftware != EmailSoftware.Outlook)
			{
				if (emailSoftware == EmailSoftware.MailTo)
				{
					Process process = new Process();
					MailToClass.MailTo(process, to, subject, bcc, subject, body, attachments);
					emailResult.Worked = process.Start();
				}
				return emailResult;
			}
			return EmailOut.SendEmailOutlook(ref additionalParameters[0], to, subject, cc, bcc, attachments, body, bodyIsHtml);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000010A8
		public static EmailResult SendEmailOutlook(ref object objApp_Late, string to, string subject, string cc, string bcc, string attachments, string body, bool bodyIsHtml)
		{
			return EmailOut.SendEmailOutlook(ref objApp_Late, null, to, subject, cc, bcc, attachments, body, bodyIsHtml);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C8 File Offset: 0x000010C8
		public static EmailResult SendEmailOutlook(ref object objApp_Late, string from, string to, string subject, string cc, string bcc, string attachments, string body, bool bodyIsHtml)
		{
			EmailResult emailResult = new EmailResult(false, null);
			try
			{
				if (objApp_Late == null)
				{
					Type typeFromProgID = Type.GetTypeFromProgID("Outlook.Application", true);
					objApp_Late = Activator.CreateInstance(typeFromProgID);
				}
				object obj = objApp_Late.GetType().InvokeMember("CreateItem", BindingFlags.InvokeMethod, null, objApp_Late, new object[]
				{
					0
				});
				if (!string.IsNullOrEmpty(from))
				{
					try
					{
						obj.GetType().InvokeMember("SentOnBehalfOfName", BindingFlags.SetProperty, null, obj, new object[]
						{
							from
						});
					}
					catch
					{
					}
				}
				if (subject != null && subject.Length > 0)
				{
					obj.GetType().InvokeMember("Subject", BindingFlags.SetProperty, null, obj, new object[]
					{
						subject
					});
				}
				if (to != null && to.Length > 0)
				{
					obj.GetType().InvokeMember("To", BindingFlags.SetProperty, null, obj, new object[]
					{
						to
					});
				}
				if (cc != null && cc.Length > 0)
				{
					obj.GetType().InvokeMember("CC", BindingFlags.SetProperty, null, obj, new object[]
					{
						cc
					});
				}
				if (bcc != null && bcc.Length > 0)
				{
					obj.GetType().InvokeMember("BCC", BindingFlags.SetProperty, null, obj, new object[]
					{
						bcc
					});
				}
				if (body != null && body.Length > 0)
				{
					string newLine = Environment.NewLine;
					if (body.IndexOf(newLine) == 0 && body.Length > newLine.Length)
					{
						body = body.Substring(newLine.Length);
					}
					if (body.Contains("%0D%0A"))
					{
						body = body.Replace(Environment.NewLine, "");
						body = body.Replace("%0D%0A", "<br />");
					}
					if (body.IndexOf("<br />") < 0)
					{
						body = body.Replace(Environment.NewLine, "<br />");
					}
					body = body.Replace("&gt;", ">").Replace("&lt;", "<");
					obj.GetType().InvokeMember("HTMLBody", BindingFlags.SetProperty, null, obj, new object[]
					{
						body
					});
				}
				object obj2 = obj.GetType().InvokeMember("Attachments", BindingFlags.InvokeMethod, null, obj, new object[0]);
				string[] array = (attachments.IndexOf(',') >= 0) ? attachments.Split(new char[]
				{
					','
				}) : attachments.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					if (text2.Length > 0 && File.Exists(text2))
					{
						obj2.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, obj2, new object[]
						{
							text2
						});
					}
				}
				obj.GetType().InvokeMember("Display", BindingFlags.InvokeMethod, null, obj, new object[]
				{
					objApp_Late
				});
				obj = null;
				objApp_Late = null;
				emailResult.Worked = true;
			}
			catch (Exception exception)
			{
				emailResult.Exception = exception;
			}
			return emailResult;
		}
	}
}
