using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Authentication;
using TechnoPro.Common.Core.Mappers.Membership;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006B RID: 107
	public class MembershipServiceManager : IMembership, IService, IConnectivity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00012D42 File Offset: 0x00010F42
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00012D4A File Offset: 0x00010F4A
		private ClientParameters Parameters { get; set; }

		// Token: 0x060003F1 RID: 1009 RVA: 0x00012D53 File Offset: 0x00010F53
		public MembershipServiceManager(ClientParameters parameters)
		{
			this.Parameters = parameters;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012D68 File Offset: 0x00010F68
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00012D7C File Offset: 0x00010F7C
		public LogonResult Logon(Credential credential)
		{
			AuthenticationSession ticket;
			return ObjectFactory.Resolve<IMembershipManager>().TryLogon(credential.Username.ToUpper(), credential.Password, this.Parameters, out ticket) ? this.CreateLogonResult(ticket) : null;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00012DC0 File Offset: 0x00010FC0
		public LogonResult LogonSSO(LogonSSOReq request)
		{
			AuthenticationSession ticket;
			return ObjectFactory.Resolve<IMembershipManager>().TryLogon(request.HashAuthentication.ToDomainObject(), this.Parameters, out ticket) ? this.CreateLogonResult(ticket) : null;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00012DFC File Offset: 0x00010FFC
		public LogonResult LogonAsUser(Credential credential, string logonAsUsername)
		{
			AuthenticationSession ticket;
			return ObjectFactory.Resolve<IMembershipManager>().TryLogon(credential.Username.ToUpper(), credential.Password, logonAsUsername, this.Parameters, out ticket) ? this.CreateLogonResult(ticket) : null;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00012E40 File Offset: 0x00011040
		public AuthTicketResult Validate(Token token)
		{
			AuthTicket authTicket = ObjectFactory.Resolve<IMembershipManager>().Authenticate(new Guid(token.SessionId));
			AuthTicketResult result;
			if (authTicket != null && authTicket.IsValid)
			{
				AuthTicketResult authTicketResult = new AuthTicketResult();
				authTicketResult.Username = authTicket.Username;
				authTicketResult.IsSessionBased = authTicket.IsSessionBased;
				authTicketResult.SessionTicket = authTicket.SessionTicket.ToString();
				result = authTicketResult;
				authTicketResult.UserRoles = authTicket.User.Roles.GetCSVRoles();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00012EC8 File Offset: 0x000110C8
		public void Logout(Token token)
		{
			bool flag = token != null && !string.IsNullOrEmpty(token.SessionId);
			if (flag)
			{
				ObjectFactory.Resolve<IMembershipManager>().Logout(new Guid(token.SessionId), this.Parameters);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00012F0C File Offset: 0x0001110C
		public ChangeUserPasswordResp ChangeUserPassword(ChangeUserPasswordReq Request)
		{
			string message;
			bool passwordChangeWasSuccessful = ObjectFactory.Resolve<IMembershipManager>().ChangeUserPassword(Request.UserName, Request.CurrentPassword, Request.NewPassword, out message);
			return new ChangeUserPasswordResp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012F54 File Offset: 0x00011154
		public UserMustChangePasswordResp UserMustChangePassword(UserMustChangePasswordReq Request)
		{
			return new UserMustChangePasswordResp
			{
				UserMustChangePassword = ObjectFactory.Resolve<IMembershipManager>().UserMustChangePassword(Request.UserName)
			};
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012F84 File Offset: 0x00011184
		public ChangeUserPasswordByAdminResp ChangeUserPasswordByAdmin(ChangeUserPasswordByAdminReq Request)
		{
			string message;
			bool passwordChangeWasSuccessful = ObjectFactory.Resolve<IMembershipManager>().ChangeUserPasswordByAdmin(new Guid(Request.AdminToken.SessionId), Request.UserName, Request.NewPassword, out message);
			return new ChangeUserPasswordByAdminResp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00012FD4 File Offset: 0x000111D4
		private LogonResult CreateLogonResult(AuthenticationSession ticket)
		{
			List<int> list = new List<int>();
			bool flag = ticket.User.Roles != null;
			if (flag)
			{
				list.AddRange(from role in ticket.User.Roles
				select role.Id);
			}
			return new LogonResult
			{
				SessionTicket = new Token
				{
					SessionId = ticket.Id.ToString()
				},
				FullName = ticket.User.FullName,
				PersonId = ticket.User.UserId,
				FirstName = ticket.User.FirstName,
				LastName = ticket.User.LastName,
				RoleIds = list,
				TokenStatus = ((ticket.TokenStatus == null) ? null : ticket.TokenStatus.ToDTO()),
				RequirePasswordChange = (ticket != null && ticket.User != null && ticket.User.RequirePasswordChange)
			};
		}
	}
}
