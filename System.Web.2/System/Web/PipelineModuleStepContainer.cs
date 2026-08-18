using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x020000E6 RID: 230
	internal sealed class PipelineModuleStepContainer
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x000030B5 File Offset: 0x000012B5
		internal PipelineModuleStepContainer()
		{
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00028AC0 File Offset: 0x00026CC0
		private List<HttpApplication.IExecutionStep> GetStepArray(RequestNotification notification, bool isPostEvent)
		{
			List<HttpApplication.IExecutionStep>[] array = this._moduleSteps;
			if (isPostEvent)
			{
				array = this._modulePostSteps;
			}
			int num = PipelineModuleStepContainer.EventToIndex(notification);
			return array[num];
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00028AEC File Offset: 0x00026CEC
		internal int GetEventCount(RequestNotification notification, bool isPostEvent)
		{
			List<HttpApplication.IExecutionStep> stepArray = this.GetStepArray(notification, isPostEvent);
			if (stepArray == null)
			{
				return 0;
			}
			return stepArray.Count;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00028B10 File Offset: 0x00026D10
		internal HttpApplication.IExecutionStep GetNextEvent(RequestNotification notification, bool isPostEvent, int eventIndex)
		{
			List<HttpApplication.IExecutionStep> stepArray = this.GetStepArray(notification, isPostEvent);
			return stepArray[eventIndex];
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00028B30 File Offset: 0x00026D30
		internal void RemoveEvent(RequestNotification notification, bool isPostEvent, Delegate handler)
		{
			List<HttpApplication.IExecutionStep>[] array = this._moduleSteps;
			if (isPostEvent)
			{
				array = this._modulePostSteps;
			}
			if (array == null)
			{
				return;
			}
			int num = PipelineModuleStepContainer.EventToIndex(notification);
			List<HttpApplication.IExecutionStep> list = array[num];
			if (list == null)
			{
				return;
			}
			int num2 = -1;
			for (int i = 0; i < list.Count; i++)
			{
				HttpApplication.SyncEventExecutionStep syncEventExecutionStep = list[i] as HttpApplication.SyncEventExecutionStep;
				if (syncEventExecutionStep != null && syncEventExecutionStep.Handler == (EventHandler)handler)
				{
					num2 = i;
					break;
				}
			}
			if (num2 != -1)
			{
				list.RemoveAt(num2);
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00028BB0 File Offset: 0x00026DB0
		internal void AddEvent(RequestNotification notification, bool isPostEvent, HttpApplication.IExecutionStep step)
		{
			int num = PipelineModuleStepContainer.EventToIndex(notification);
			List<HttpApplication.IExecutionStep>[] array;
			if (isPostEvent)
			{
				if (this._modulePostSteps == null)
				{
					this._modulePostSteps = new List<HttpApplication.IExecutionStep>[32];
				}
				array = this._modulePostSteps;
			}
			else
			{
				if (this._moduleSteps == null)
				{
					this._moduleSteps = new List<HttpApplication.IExecutionStep>[32];
				}
				array = this._moduleSteps;
			}
			List<HttpApplication.IExecutionStep> list = array[num];
			if (list == null)
			{
				list = new List<HttpApplication.IExecutionStep>();
				array[num] = list;
			}
			list.Add(step);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00028C1C File Offset: 0x00026E1C
		private static int EventToIndex(RequestNotification notification)
		{
			int result = -1;
			if (notification <= RequestNotification.PreExecuteRequestHandler)
			{
				if (notification <= RequestNotification.ResolveRequestCache)
				{
					switch (notification)
					{
					case RequestNotification.BeginRequest:
						return 0;
					case RequestNotification.AuthenticateRequest:
						return 1;
					case RequestNotification.BeginRequest | RequestNotification.AuthenticateRequest:
						break;
					case RequestNotification.AuthorizeRequest:
						return 2;
					default:
						if (notification == RequestNotification.ResolveRequestCache)
						{
							return 3;
						}
						break;
					}
				}
				else
				{
					if (notification == RequestNotification.MapRequestHandler)
					{
						return 4;
					}
					if (notification == RequestNotification.AcquireRequestState)
					{
						return 5;
					}
					if (notification == RequestNotification.PreExecuteRequestHandler)
					{
						return 6;
					}
				}
			}
			else if (notification <= RequestNotification.UpdateRequestCache)
			{
				if (notification == RequestNotification.ExecuteRequestHandler)
				{
					return 7;
				}
				if (notification == RequestNotification.ReleaseRequestState)
				{
					return 8;
				}
				if (notification == RequestNotification.UpdateRequestCache)
				{
					return 9;
				}
			}
			else
			{
				if (notification == RequestNotification.LogRequest)
				{
					return 10;
				}
				if (notification == RequestNotification.EndRequest)
				{
					return 11;
				}
				if (notification == RequestNotification.SendResponse)
				{
					return 12;
				}
			}
			return result;
		}

		// Token: 0x0400055C RID: 1372
		private List<HttpApplication.IExecutionStep>[] _moduleSteps;

		// Token: 0x0400055D RID: 1373
		private List<HttpApplication.IExecutionStep>[] _modulePostSteps;
	}
}
