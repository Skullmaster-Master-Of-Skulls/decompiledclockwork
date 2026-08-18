using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Mail
{
	// Token: 0x02000116 RID: 278
	[Obsolete("The recommended alternative is System.Net.Mail.SmtpClient. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class SmtpMail
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x000030B5 File Offset: 0x000012B5
		private SmtpMail()
		{
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00030700 File Offset: 0x0002E900
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0003071D File Offset: 0x0002E91D
		public static string SmtpServer
		{
			get
			{
				string server = SmtpMail._server;
				if (server == null)
				{
					return string.Empty;
				}
				return server;
			}
			set
			{
				SmtpMail._server = value;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00030728 File Offset: 0x0002E928
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static void Send(string from, string to, string subject, string messageText)
		{
			object lockObject = SmtpMail._lockObject;
			lock (lockObject)
			{
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
				}
				if (!SmtpMail.CdoSysHelper.OsSupportsCdoSys())
				{
					throw new PlatformNotSupportedException(SR.GetString("SmtpMail_not_supported_on_Win7_and_higher"));
				}
				if (Environment.OSVersion.Version.Major <= 4)
				{
					SmtpMail.CdoNtsHelper.Send(from, to, subject, messageText);
				}
				else
				{
					SmtpMail.CdoSysHelper.Send(from, to, subject, messageText);
				}
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000307BC File Offset: 0x0002E9BC
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static void Send(MailMessage message)
		{
			object lockObject = SmtpMail._lockObject;
			lock (lockObject)
			{
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
				}
				if (!SmtpMail.CdoSysHelper.OsSupportsCdoSys())
				{
					throw new PlatformNotSupportedException(SR.GetString("SmtpMail_not_supported_on_Win7_and_higher"));
				}
				if (Environment.OSVersion.Version.Major <= 4)
				{
					SmtpMail.CdoNtsHelper.Send(message);
				}
				else
				{
					SmtpMail.CdoSysHelper.Send(message);
				}
			}
		}

		// Token: 0x040013BD RID: 5053
		private static object _lockObject = new object();

		// Token: 0x040013BE RID: 5054
		private static string _server;

		// Token: 0x020008F9 RID: 2297
		internal class LateBoundAccessHelper
		{
			// Token: 0x06006882 RID: 26754 RVA: 0x00174039 File Offset: 0x00172239
			internal LateBoundAccessHelper(string progId)
			{
				this._progId = progId;
			}

			// Token: 0x17001D04 RID: 7428
			// (get) Token: 0x06006883 RID: 26755 RVA: 0x00174048 File Offset: 0x00172248
			private Type LateBoundType
			{
				get
				{
					if (this._type == null)
					{
						try
						{
							this._type = Type.GetTypeFromProgID(this._progId);
						}
						catch
						{
						}
						if (this._type == null)
						{
							throw new HttpException(SR.GetString("SMTP_TypeCreationError", new object[]
							{
								this._progId
							}));
						}
					}
					return this._type;
				}
			}

			// Token: 0x06006884 RID: 26756 RVA: 0x001740BC File Offset: 0x001722BC
			internal object CreateInstance()
			{
				return Activator.CreateInstance(this.LateBoundType);
			}

			// Token: 0x06006885 RID: 26757 RVA: 0x001740CC File Offset: 0x001722CC
			internal object CallMethod(object obj, string methodName, object[] args)
			{
				object result;
				try
				{
					result = SmtpMail.LateBoundAccessHelper.CallMethod(this.LateBoundType, obj, methodName, args);
				}
				catch (Exception ex)
				{
					throw new HttpException(SmtpMail.LateBoundAccessHelper.GetInnerMostException(ex).Message, ex);
				}
				return result;
			}

			// Token: 0x06006886 RID: 26758 RVA: 0x00174110 File Offset: 0x00172310
			internal static object CallMethodStatic(object obj, string methodName, object[] args)
			{
				return SmtpMail.LateBoundAccessHelper.CallMethod(obj.GetType(), obj, methodName, args);
			}

			// Token: 0x06006887 RID: 26759 RVA: 0x00174120 File Offset: 0x00172320
			private static object CallMethod(Type type, object obj, string methodName, object[] args)
			{
				return type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, args, CultureInfo.InvariantCulture);
			}

			// Token: 0x06006888 RID: 26760 RVA: 0x00174136 File Offset: 0x00172336
			private static Exception GetInnerMostException(Exception e)
			{
				if (e.InnerException == null)
				{
					return e;
				}
				return SmtpMail.LateBoundAccessHelper.GetInnerMostException(e.InnerException);
			}

			// Token: 0x06006889 RID: 26761 RVA: 0x00174150 File Offset: 0x00172350
			internal object GetProp(object obj, string propName)
			{
				object prop;
				try
				{
					prop = SmtpMail.LateBoundAccessHelper.GetProp(this.LateBoundType, obj, propName);
				}
				catch (Exception ex)
				{
					throw new HttpException(SmtpMail.LateBoundAccessHelper.GetInnerMostException(ex).Message, ex);
				}
				return prop;
			}

			// Token: 0x0600688A RID: 26762 RVA: 0x00174194 File Offset: 0x00172394
			internal static object GetPropStatic(object obj, string propName)
			{
				return SmtpMail.LateBoundAccessHelper.GetProp(obj.GetType(), obj, propName);
			}

			// Token: 0x0600688B RID: 26763 RVA: 0x001741A3 File Offset: 0x001723A3
			private static object GetProp(Type type, object obj, string propName)
			{
				return type.InvokeMember(propName, BindingFlags.GetProperty, null, obj, new object[0], CultureInfo.InvariantCulture);
			}

			// Token: 0x0600688C RID: 26764 RVA: 0x001741C0 File Offset: 0x001723C0
			internal void SetProp(object obj, string propName, object propValue)
			{
				try
				{
					SmtpMail.LateBoundAccessHelper.SetProp(this.LateBoundType, obj, propName, propValue);
				}
				catch (Exception ex)
				{
					throw new HttpException(SmtpMail.LateBoundAccessHelper.GetInnerMostException(ex).Message, ex);
				}
			}

			// Token: 0x0600688D RID: 26765 RVA: 0x00174200 File Offset: 0x00172400
			internal static void SetPropStatic(object obj, string propName, object propValue)
			{
				SmtpMail.LateBoundAccessHelper.SetProp(obj.GetType(), obj, propName, propValue);
			}

			// Token: 0x0600688E RID: 26766 RVA: 0x00174210 File Offset: 0x00172410
			private static void SetProp(Type type, object obj, string propName, object propValue)
			{
				if (propValue != null && propValue is string && ((string)propValue).IndexOf('\0') >= 0)
				{
					throw new ArgumentException();
				}
				type.InvokeMember(propName, BindingFlags.SetProperty, null, obj, new object[]
				{
					propValue
				}, CultureInfo.InvariantCulture);
			}

			// Token: 0x0600688F RID: 26767 RVA: 0x0017425C File Offset: 0x0017245C
			internal void SetProp(object obj, string propName, object propKey, object propValue)
			{
				try
				{
					SmtpMail.LateBoundAccessHelper.SetProp(this.LateBoundType, obj, propName, propKey, propValue);
				}
				catch (Exception ex)
				{
					throw new HttpException(SmtpMail.LateBoundAccessHelper.GetInnerMostException(ex).Message, ex);
				}
			}

			// Token: 0x06006890 RID: 26768 RVA: 0x001742A0 File Offset: 0x001724A0
			internal static void SetPropStatic(object obj, string propName, object propKey, object propValue)
			{
				SmtpMail.LateBoundAccessHelper.SetProp(obj.GetType(), obj, propName, propKey, propValue);
			}

			// Token: 0x06006891 RID: 26769 RVA: 0x001742B4 File Offset: 0x001724B4
			private static void SetProp(Type type, object obj, string propName, object propKey, object propValue)
			{
				if (propValue != null && propValue is string && ((string)propValue).IndexOf('\0') >= 0)
				{
					throw new ArgumentException();
				}
				type.InvokeMember(propName, BindingFlags.SetProperty, null, obj, new object[]
				{
					propKey,
					propValue
				}, CultureInfo.InvariantCulture);
			}

			// Token: 0x040036DF RID: 14047
			private string _progId;

			// Token: 0x040036E0 RID: 14048
			private Type _type;
		}

		// Token: 0x020008FA RID: 2298
		internal class CdoNtsHelper
		{
			// Token: 0x06006892 RID: 26770 RVA: 0x000030B5 File Offset: 0x000012B5
			private CdoNtsHelper()
			{
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x00174308 File Offset: 0x00172508
			internal static void Send(MailMessage message)
			{
				object obj = SmtpMail.CdoNtsHelper._helper.CreateInstance();
				if (message.From != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "From", message.From);
				}
				if (message.To != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "To", message.To);
				}
				if (message.Cc != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Cc", message.Cc);
				}
				if (message.Bcc != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Bcc", message.Bcc);
				}
				if (message.Subject != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Subject", message.Subject);
				}
				if (message.Priority != MailPriority.Normal)
				{
					int num = 0;
					switch (message.Priority)
					{
					case MailPriority.Normal:
						num = 1;
						break;
					case MailPriority.Low:
						num = 0;
						break;
					case MailPriority.High:
						num = 2;
						break;
					}
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Importance", num);
				}
				if (message.BodyEncoding != null)
				{
					SmtpMail.CdoNtsHelper._helper.CallMethod(obj, "SetLocaleIDs", new object[]
					{
						message.BodyEncoding.CodePage
					});
				}
				if (message.UrlContentBase != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "ContentBase", message.UrlContentBase);
				}
				if (message.UrlContentLocation != null)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "ContentLocation", message.UrlContentLocation);
				}
				int count = message.Headers.Count;
				if (count > 0)
				{
					IDictionaryEnumerator enumerator = message.Headers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string propKey = (string)enumerator.Key;
						string propValue = (string)enumerator.Value;
						SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Value", propKey, propValue);
					}
				}
				if (message.BodyFormat == MailFormat.Html)
				{
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "BodyFormat", 0);
					SmtpMail.CdoNtsHelper._helper.SetProp(obj, "MailFormat", 0);
				}
				SmtpMail.CdoNtsHelper._helper.SetProp(obj, "Body", (message.Body != null) ? message.Body : string.Empty);
				foreach (object obj2 in message.Attachments)
				{
					MailAttachment mailAttachment = (MailAttachment)obj2;
					int num2 = 0;
					MailEncoding encoding = mailAttachment.Encoding;
					if (encoding != MailEncoding.UUEncode)
					{
						if (encoding == MailEncoding.Base64)
						{
							num2 = 1;
						}
					}
					else
					{
						num2 = 0;
					}
					SmtpMail.CdoNtsHelper._helper.CallMethod(obj, "AttachFile", new object[]
					{
						mailAttachment.Filename,
						null,
						num2
					});
				}
				SmtpMail.CdoNtsHelper._helper.CallMethod(obj, "Send", new object[5]);
				Marshal.ReleaseComObject(obj);
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x001745AC File Offset: 0x001727AC
			internal static void Send(string from, string to, string subject, string messageText)
			{
				SmtpMail.CdoNtsHelper.Send(new MailMessage
				{
					From = from,
					To = to,
					Subject = subject,
					Body = messageText
				});
			}

			// Token: 0x040036E1 RID: 14049
			private static SmtpMail.LateBoundAccessHelper _helper = new SmtpMail.LateBoundAccessHelper("CDONTS.NewMail");
		}

		// Token: 0x020008FB RID: 2299
		internal class CdoSysHelper
		{
			// Token: 0x06006896 RID: 26774 RVA: 0x000030B5 File Offset: 0x000012B5
			private CdoSysHelper()
			{
			}

			// Token: 0x06006897 RID: 26775 RVA: 0x001745F4 File Offset: 0x001727F4
			private static void SetField(object m, string name, string value)
			{
				SmtpMail.CdoSysHelper._helper.SetProp(m, "Fields", "urn:schemas:mailheader:" + name, value);
				object prop = SmtpMail.CdoSysHelper._helper.GetProp(m, "Fields");
				SmtpMail.LateBoundAccessHelper.CallMethodStatic(prop, "Update", new object[0]);
				Marshal.ReleaseComObject(prop);
			}

			// Token: 0x06006898 RID: 26776 RVA: 0x00174648 File Offset: 0x00172848
			private static bool CdoSysExists()
			{
				if (SmtpMail.CdoSysHelper.cdoSysLibraryInfo != SmtpMail.CdoSysHelper.CdoSysLibraryStatus.NotChecked)
				{
					return SmtpMail.CdoSysHelper.cdoSysLibraryInfo == SmtpMail.CdoSysHelper.CdoSysLibraryStatus.Exists;
				}
				string systemDllFullPath = PathUtil.GetSystemDllFullPath("cdosys.dll");
				IntPtr intPtr = UnsafeNativeMethods.LoadLibrary(systemDllFullPath);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.FreeLibrary(intPtr);
					SmtpMail.CdoSysHelper.cdoSysLibraryInfo = SmtpMail.CdoSysHelper.CdoSysLibraryStatus.Exists;
					return true;
				}
				SmtpMail.CdoSysHelper.cdoSysLibraryInfo = SmtpMail.CdoSysHelper.CdoSysLibraryStatus.DoesntExist;
				return false;
			}

			// Token: 0x06006899 RID: 26777 RVA: 0x0017469C File Offset: 0x0017289C
			internal static bool OsSupportsCdoSys()
			{
				Version version = Environment.OSVersion.Version;
				return (version.Major < 7 && (version.Major != 6 || version.Minor < 1)) || SmtpMail.CdoSysHelper.CdoSysExists();
			}

			// Token: 0x0600689A RID: 26778 RVA: 0x001746D8 File Offset: 0x001728D8
			internal static void Send(MailMessage message)
			{
				object obj = SmtpMail.CdoSysHelper._helper.CreateInstance();
				if (message.From != null)
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "From", message.From);
				}
				if (message.To != null)
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "To", message.To);
				}
				if (message.Cc != null)
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "Cc", message.Cc);
				}
				if (message.Bcc != null)
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "Bcc", message.Bcc);
				}
				if (message.Subject != null)
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "Subject", message.Subject);
				}
				if (message.Priority != MailPriority.Normal)
				{
					string text = null;
					switch (message.Priority)
					{
					case MailPriority.Normal:
						text = "normal";
						break;
					case MailPriority.Low:
						text = "low";
						break;
					case MailPriority.High:
						text = "high";
						break;
					}
					if (text != null)
					{
						SmtpMail.CdoSysHelper.SetField(obj, "importance", text);
					}
				}
				if (message.BodyEncoding != null)
				{
					object prop = SmtpMail.CdoSysHelper._helper.GetProp(obj, "BodyPart");
					SmtpMail.LateBoundAccessHelper.SetPropStatic(prop, "Charset", message.BodyEncoding.BodyName);
					Marshal.ReleaseComObject(prop);
				}
				if (message.UrlContentBase != null)
				{
					SmtpMail.CdoSysHelper.SetField(obj, "content-base", message.UrlContentBase);
				}
				if (message.UrlContentLocation != null)
				{
					SmtpMail.CdoSysHelper.SetField(obj, "content-location", message.UrlContentLocation);
				}
				int count = message.Headers.Count;
				if (count > 0)
				{
					IDictionaryEnumerator enumerator = message.Headers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						SmtpMail.CdoSysHelper.SetField(obj, (string)enumerator.Key, (string)enumerator.Value);
					}
				}
				if (message.Body != null)
				{
					if (message.BodyFormat == MailFormat.Html)
					{
						SmtpMail.CdoSysHelper._helper.SetProp(obj, "HtmlBody", message.Body);
					}
					else
					{
						SmtpMail.CdoSysHelper._helper.SetProp(obj, "TextBody", message.Body);
					}
				}
				else
				{
					SmtpMail.CdoSysHelper._helper.SetProp(obj, "TextBody", string.Empty);
				}
				foreach (object obj2 in message.Attachments)
				{
					MailAttachment mailAttachment = (MailAttachment)obj2;
					SmtpMail.LateBoundAccessHelper helper = SmtpMail.CdoSysHelper._helper;
					object obj3 = obj;
					string methodName = "AddAttachment";
					object[] array = new object[3];
					array[0] = mailAttachment.Filename;
					object obj4 = helper.CallMethod(obj3, methodName, array);
					if (mailAttachment.Encoding == MailEncoding.UUEncode)
					{
						SmtpMail.CdoSysHelper._helper.SetProp(obj, "MimeFormatted", false);
					}
					if (obj4 != null)
					{
						Marshal.ReleaseComObject(obj4);
					}
				}
				string smtpServer = SmtpMail.SmtpServer;
				if (!string.IsNullOrEmpty(smtpServer) || message.Fields.Count > 0)
				{
					object propStatic = SmtpMail.LateBoundAccessHelper.GetPropStatic(obj, "Configuration");
					if (propStatic != null)
					{
						SmtpMail.LateBoundAccessHelper.SetPropStatic(propStatic, "Fields", "http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
						SmtpMail.LateBoundAccessHelper.SetPropStatic(propStatic, "Fields", "http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
						if (!string.IsNullOrEmpty(smtpServer))
						{
							SmtpMail.LateBoundAccessHelper.SetPropStatic(propStatic, "Fields", "http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
						}
						foreach (object obj5 in message.Fields)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj5;
							SmtpMail.LateBoundAccessHelper.SetPropStatic(propStatic, "Fields", (string)dictionaryEntry.Key, dictionaryEntry.Value);
						}
						object propStatic2 = SmtpMail.LateBoundAccessHelper.GetPropStatic(propStatic, "Fields");
						SmtpMail.LateBoundAccessHelper.CallMethodStatic(propStatic2, "Update", new object[0]);
						Marshal.ReleaseComObject(propStatic2);
						Marshal.ReleaseComObject(propStatic);
					}
				}
				if (HostingEnvironment.IsHosted)
				{
					using (new ProcessImpersonationContext())
					{
						SmtpMail.CdoSysHelper._helper.CallMethod(obj, "Send", new object[0]);
						goto IL_3BA;
					}
				}
				SmtpMail.CdoSysHelper._helper.CallMethod(obj, "Send", new object[0]);
				IL_3BA:
				Marshal.ReleaseComObject(obj);
			}

			// Token: 0x0600689B RID: 26779 RVA: 0x00174AC4 File Offset: 0x00172CC4
			internal static void Send(string from, string to, string subject, string messageText)
			{
				SmtpMail.CdoSysHelper.Send(new MailMessage
				{
					From = from,
					To = to,
					Subject = subject,
					Body = messageText
				});
			}

			// Token: 0x040036E2 RID: 14050
			private static SmtpMail.LateBoundAccessHelper _helper = new SmtpMail.LateBoundAccessHelper("CDO.Message");

			// Token: 0x040036E3 RID: 14051
			private static SmtpMail.CdoSysHelper.CdoSysLibraryStatus cdoSysLibraryInfo = SmtpMail.CdoSysHelper.CdoSysLibraryStatus.NotChecked;

			// Token: 0x02000A86 RID: 2694
			private enum CdoSysLibraryStatus
			{
				// Token: 0x04003BC9 RID: 15305
				NotChecked,
				// Token: 0x04003BCA RID: 15306
				Exists,
				// Token: 0x04003BCB RID: 15307
				DoesntExist
			}
		}
	}
}
