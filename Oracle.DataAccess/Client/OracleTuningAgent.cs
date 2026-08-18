using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	internal class OracleTuningAgent
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0003255C File Offset: 0x0003155C
		static OracleTuningAgent()
		{
			try
			{
				long num = -1L;
				long installedRAM = -1L;
				if (OpsCom.GetSystemMemoryInfo(ref num, ref installedRAM) == 0)
				{
					OracleTuningAgent.m_isUsableMemInfoAvail = true;
					OracleTuningAgent.m_highMem = (long)(0.7f * (float)num);
					OracleTuningAgent.m_veryHighMem = (long)(0.8f * (float)num);
					OracleTuningAgent.m_installedRAM = installedRAM;
					OracleTuningAgent.m_minRAMReqdForTuning = (long)(0.3f * (float)OracleTuningAgent.m_installedRAM);
					OracleTuningAgent.m_minRAMNeeded = (long)(0.2f * (float)OracleTuningAgent.m_installedRAM);
				}
				else if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::OracleTuningAgent(): Virtual Memory Information not available \n"
					});
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::OracleTuningAgent(): Exception : " + ex.ToString() + " \n"
					});
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000326A8 File Offset: 0x000316A8
		internal static void Register(string connectionPoolString, string poolName, int poolId, OracleTuningAgent.UpdateRecommendations updateRecommendationsDeleg, OracleTuningAgent.IncrementStmtSamplesLimit incrementStmtSamplesDeleg, out int agentKey)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTuningAgent::Register()\n"
				});
			}
			agentKey = -1;
			if (!OracleTuningAgent.m_isUsableMemInfoAvail)
			{
				return;
			}
			lock (OracleTuningAgent.m_registrationLock)
			{
				lock (OracleTuningAgent.m_input)
				{
					OracleTuningAgent.AgentInput agentInput = null;
					for (int i = 0; i < OracleTuningAgent.m_input.Count; i++)
					{
						OracleTuningAgent.AgentInput agentInput2 = OracleTuningAgent.m_input[i] as OracleTuningAgent.AgentInput;
						if (agentInput2 != null && agentInput2.m_poolConnectionString == connectionPoolString)
						{
							if (!agentInput2.m_registered)
							{
								OracleTuningAgent.m_numberOfRegistrations++;
								agentInput2.m_registered = true;
							}
							agentInput2.m_UpdateRecommendationsDeleg = updateRecommendationsDeleg;
							agentInput2.m_incrementStmtSamplesDeleg = incrementStmtSamplesDeleg;
							agentInput = agentInput2;
						}
					}
					if (agentInput == null)
					{
						agentInput = new OracleTuningAgent.AgentInput();
						agentInput.m_poolConnectionString = connectionPoolString;
						agentInput.m_poolId = poolId;
						agentInput.m_UpdateRecommendationsDeleg = updateRecommendationsDeleg;
						agentInput.m_incrementStmtSamplesDeleg = incrementStmtSamplesDeleg;
						OracleTuningAgent.m_input.Add(agentInput);
						agentInput.m_agentKey = OracleTuningAgent.m_input.Count - 1;
						OracleTuningAgent.m_numberOfRegistrations++;
					}
					agentKey = agentInput.m_agentKey;
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(64U, new string[]
						{
							string.Concat(new object[]
							{
								" (TUNING) OracleTuningAgent::Register(): Registered pool \"",
								poolName,
								"\" with pool Id ",
								poolId,
								"\n"
							})
						});
					}
				}
				if (OracleTuningAgent.m_numberOfRegistrations == 1)
				{
					OracleTuningAgent.m_allPoolsHaveUnregistered = false;
					if (OracleTuningAgent.m_tuningThread == null)
					{
						try
						{
							ThreadStart start = new ThreadStart(OracleTuningAgent.TuningFunction);
							OracleTuningAgent.m_tuningThread = new Thread(start);
							OracleTuningAgent.m_tuningThread.IsBackground = true;
							OracleTuningAgent.m_tuningThread.Start();
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									" (TUNING) OracleTuningAgent::Register(): Tuning thread started.\n"
								});
							}
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									" (ERROR) OracleTuningAgent::Register(): Error in starting Tuning Thread : " + ex.ToString() + " \n"
								});
							}
						}
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleTuningAgent::Register()\n"
				});
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00032944 File Offset: 0x00031944
		internal static void Unregister(int agentKey)
		{
			lock (OracleTuningAgent.m_registrationLock)
			{
				lock (OracleTuningAgent.m_input)
				{
					OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[agentKey] as OracleTuningAgent.AgentInput;
					if (agentInput.m_registered)
					{
						OracleTuningAgent.m_numberOfRegistrations--;
						agentInput.m_registered = false;
					}
				}
				if (OracleTuningAgent.m_numberOfRegistrations == 0)
				{
					OracleTuningAgent.m_allPoolsHaveUnregistered = true;
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000329E0 File Offset: 0x000319E0
		internal static void TuningFunction()
		{
			try
			{
				for (;;)
				{
					IL_00:
					if (OracleTuningAgent.m_allPoolsHaveUnregistered)
					{
						OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.INIT;
						Thread.Sleep(10000);
					}
					else
					{
						switch (OracleTuningAgent.m_agentState)
						{
						case OracleTuningAgent.AgentState.INIT:
							OracleTuningAgent.DoInitialization();
							break;
						case OracleTuningAgent.AgentState.WAIT:
							OracleTuningAgent.DoWait();
							break;
						case OracleTuningAgent.AgentState.SCAN:
							OracleTuningAgent.DoScan();
							break;
						case OracleTuningAgent.AgentState.REDUCE:
							OracleTuningAgent.DoReduce();
							break;
						case OracleTuningAgent.AgentState.OPTIMIZE:
							OracleTuningAgent.DoOptimize();
							break;
						case OracleTuningAgent.AgentState.WATCH:
							OracleTuningAgent.DoWatch();
							break;
						case OracleTuningAgent.AgentState.REVERT:
							OracleTuningAgent.DoRevert();
							break;
						default:
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									" (ERROR) OracleTuningAgent::TuningFunction(): Unrecognized agent state " + OracleTuningAgent.m_agentState + " \n"
								});
							}
							break;
						}
					}
				}
			}
			catch (ThreadAbortException ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::TuningFunction(): Error : " + ex.ToString() + " \n"
					});
				}
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::TuningFunction(): Error : " + ex2.ToString() + " \n"
					});
				}
				OracleTuningAgent.m_selectedInput = null;
				OracleTuningAgent.m_selectedInputIndex = -1;
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
				goto IL_00;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00032B34 File Offset: 0x00031B34
		private static void DoInitialization()
		{
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.WAIT;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00032B3C File Offset: 0x00031B3C
		private static void DoWait()
		{
			int num = 0;
			int num2;
			do
			{
				Thread.Sleep(5000);
				int count = OracleTuningAgent.m_input.Count;
				num2 = count - num;
				num = count;
			}
			while (num2 <= 0);
			Thread.Sleep(5000);
			OracleTuningAgent.m_scanCyclesToSkip = 0;
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00032B84 File Offset: 0x00031B84
		private static void DoScan()
		{
			Thread.Sleep(10000 * OracleTuningAgent.m_scanCyclesToSkip);
			long currentVirtualMemorySize = OracleTuningAgent.GetCurrentVirtualMemorySize();
			long num = -1L;
			int availPhysMemory = OpsCom.GetAvailPhysMemory(ref num);
			if (availPhysMemory != 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::DoScan(): Available Physical Memory Information not found \n"
					});
				}
				return;
			}
			if (currentVirtualMemorySize < OracleTuningAgent.m_highMem && num > OracleTuningAgent.m_minRAMReqdForTuning)
			{
				OracleTuningAgent.bHighMemoryAlertFlag = false;
				OracleTuningAgent.SelectPoolToOptimize();
				if (OracleTuningAgent.m_selectedInput == null)
				{
					Thread.Sleep(10000 * OracleTuningAgent.m_scanCyclesToSkip);
					OracleTuningAgent.m_scanCyclesToSkip = 1;
					OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
					return;
				}
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.OPTIMIZE;
				return;
			}
			else
			{
				if (currentVirtualMemorySize > OracleTuningAgent.m_veryHighMem || num < OracleTuningAgent.m_minRAMNeeded)
				{
					if (currentVirtualMemorySize > OracleTuningAgent.m_veryHighMem)
					{
						OracleTuningAgent.m_memoryConsumptionToReduce = currentVirtualMemorySize - OracleTuningAgent.m_veryHighMem;
					}
					else
					{
						OracleTuningAgent.m_memoryConsumptionToReduce = 0L;
					}
					if (num < OracleTuningAgent.m_minRAMNeeded && OracleTuningAgent.m_memoryConsumptionToReduce < OracleTuningAgent.m_minRAMNeeded - num)
					{
						OracleTuningAgent.m_memoryConsumptionToReduce = OracleTuningAgent.m_minRAMNeeded - num;
					}
					OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.REDUCE;
					if (!OracleTuningAgent.bHighMemoryAlertFlag)
					{
						OracleTuningAgent.ReleaseSamplesFromAllAgentInputs();
					}
					OracleTuningAgent.bHighMemoryAlertFlag = true;
					return;
				}
				if (!OracleTuningAgent.bHighMemoryAlertFlag)
				{
					OracleTuningAgent.ReleaseSamplesFromAllAgentInputs();
				}
				OracleTuningAgent.bHighMemoryAlertFlag = true;
				OracleTuningAgent.m_scanCyclesToSkip = 1;
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
				return;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00032CA0 File Offset: 0x00031CA0
		private static void DoOptimize()
		{
			OracleTuningAgent.m_selectedInput.m_collatedData = OracleTuningAgent.CollateData();
			OracleTuningAgent.DoOptimizeSCS();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00032CB8 File Offset: 0x00031CB8
		private static void UpdateSCSTuningInfo(bool scsFESTuningDone, bool SCSUniqStmtDone)
		{
			OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[OracleTuningAgent.m_selectedInput.m_agentKey] as OracleTuningAgent.AgentInput;
			agentInput.m_scsTuningUptoFESDone = scsFESTuningDone;
			agentInput.m_scsTuningUptoUniqStmtsDone = SCSUniqStmtDone;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00032CF0 File Offset: 0x00031CF0
		private static void DoOptimizeSCS()
		{
			if (OracleTuningAgent.m_selectedInput.m_scs >= OraTrace.MaxStatementCacheSize)
			{
				if (OracleTuningAgent.m_selectedInput.m_scs > OraTrace.MaxStatementCacheSize && OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null)
				{
					int maxStatementCacheSize = OraTrace.MaxStatementCacheSize;
					OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, maxStatementCacheSize);
					OracleTuningAgent.m_selectedInput.m_scsRecommended = maxStatementCacheSize;
					OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, maxStatementCacheSize);
				}
				OracleTuningAgent.UpdateSCSTuningInfo(true, true);
				OracleTuningAgent.m_scanCyclesToSkip = 1;
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
				return;
			}
			Hashtable collatedData = OracleTuningAgent.m_selectedInput.m_collatedData;
			if (collatedData != null)
			{
				if (!OracleTuningAgent.m_selectedInput.m_scsTuningUptoFESDone)
				{
					OracleTuningAgent.OptimizeSCSUptoFES(collatedData);
					return;
				}
				OracleTuningAgent.OptimizeSCSUptoUniqStmt(collatedData);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00032D98 File Offset: 0x00031D98
		private static void OptimizeSCSUptoUniqStmt(Hashtable data)
		{
			int num = data.Count;
			num += 5;
			if (OracleTuningAgent.m_selectedInput.m_scs < num)
			{
				try
				{
					int num2 = (int)Math.Ceiling(0.1 * (double)(num - OracleTuningAgent.m_selectedInput.m_scs));
					if (num2 < 5)
					{
						num2 = 5;
					}
					int num3 = OracleTuningAgent.m_selectedInput.m_scs + num2;
					if (num3 > num)
					{
						num3 = num;
					}
					if (OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null)
					{
						if (num3 > OraTrace.MaxStatementCacheSize)
						{
							num3 = OraTrace.MaxStatementCacheSize;
						}
						OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, num3);
						OracleTuningAgent.m_selectedInput.m_scsRecommended = num3;
						OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, num3);
					}
					OracleTuningAgent.UpdateSCSTuningInfo(true, false);
					OracleTuningAgent.m_watchCycles = 6;
					OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.WAIT;
					return;
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(64U, new string[]
						{
							" (ERROR) OracleTuningAgent::OptimizeSCSUptoUniqStmt(): ERROR: " + ex.ToString() + " \n"
						});
					}
					return;
				}
			}
			if ((double)OracleTuningAgent.m_selectedInput.m_scs > 1.1 * (double)num)
			{
				int num4 = (int)Math.Ceiling((double)(OracleTuningAgent.m_selectedInput.m_scs - num) * 0.1);
				if (num4 > 0)
				{
					int num5 = OracleTuningAgent.m_selectedInput.m_scs - num4;
					if (OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null)
					{
						if (num5 > OraTrace.MaxStatementCacheSize)
						{
							num5 = OraTrace.MaxStatementCacheSize;
						}
						OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, num5);
						OracleTuningAgent.m_selectedInput.m_scsRecommended = num5;
						OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, num5);
					}
					OracleTuningAgent.UpdateSCSTuningInfo(true, false);
				}
				else
				{
					OracleTuningAgent.UpdateSCSTuningInfo(true, true);
				}
				OracleTuningAgent.m_scanCyclesToSkip = 1;
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
				return;
			}
			OracleTuningAgent.UpdateSCSTuningInfo(true, true);
			OracleTuningAgent.m_scanCyclesToSkip = 1;
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00032F68 File Offset: 0x00031F68
		private static void OptimizeSCSUptoFES(Hashtable data)
		{
			long num = 0L;
			foreach (object obj in data.Values)
			{
				StatementDetails statementDetails = (StatementDetails)obj;
				num += (long)statementDetails.m_executionsIfNotSelect;
			}
			double num2 = (double)(num / (long)data.Count);
			int num3 = 0;
			foreach (object obj2 in data.Values)
			{
				StatementDetails statementDetails2 = (StatementDetails)obj2;
				if ((double)statementDetails2.m_executionsIfNotSelect >= num2)
				{
					num3++;
				}
			}
			int num4 = (int)(0.2 * (double)num3);
			if (num4 > 2)
			{
				num3 += num4 + 5;
			}
			else
			{
				num3 += 7;
			}
			int num5 = data.Count + 5;
			if (OracleTuningAgent.m_selectedInput.m_scs < num3 && OracleTuningAgent.m_selectedInput.m_scs < num5)
			{
				try
				{
					int num6 = (int)Math.Ceiling(0.1 * (double)(num3 - OracleTuningAgent.m_selectedInput.m_scs));
					if (num6 < 5)
					{
						num6 = 5;
					}
					int num7 = OracleTuningAgent.m_selectedInput.m_scs + num6;
					if (num7 > num3)
					{
						num7 = num3;
					}
					if (num7 > num5)
					{
						num7 = num5;
					}
					if (OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null)
					{
						if (num7 > OraTrace.MaxStatementCacheSize)
						{
							num7 = OraTrace.MaxStatementCacheSize;
						}
						OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, num7);
						OracleTuningAgent.m_selectedInput.m_scsRecommended = num7;
						OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, num7);
					}
					OracleTuningAgent.UpdateSCSTuningInfo(false, false);
					OracleTuningAgent.m_watchCycles = 6;
					OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.WATCH;
					return;
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(64U, new string[]
						{
							" (ERROR) OracleTuningAgent::OptimizeSCSUptoFES(): ERROR: " + ex.ToString() + " \n"
						});
					}
					return;
				}
			}
			OracleTuningAgent.UpdateSCSTuningInfo(true, false);
			OracleTuningAgent.m_scanCyclesToSkip = 1;
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0003317C File Offset: 0x0003217C
		private static void DoReduce()
		{
			int num = (int)Math.Ceiling((double)OracleTuningAgent.m_memoryConsumptionToReduce / 51200.0);
			ArrayList poolIDsToReduce = OracleTuningAgent.GetPoolIDsToReduce();
			if (poolIDsToReduce.Count == 0)
			{
				OracleTuningAgent.m_scanCyclesToSkip = 3;
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
				return;
			}
			int num2 = 0;
			foreach (object obj in poolIDsToReduce)
			{
				int index = (int)obj;
				num2 += (OracleTuningAgent.m_input[index] as OracleTuningAgent.AgentInput).m_noOfConnections;
			}
			int num3 = (int)Math.Ceiling((double)num / (double)num2);
			foreach (object obj2 in poolIDsToReduce)
			{
				int index2 = (int)obj2;
				OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[index2] as OracleTuningAgent.AgentInput;
				int num4 = agentInput.m_scsRecommended - num3;
				if (num4 < 30)
				{
					num4 = 30;
				}
				agentInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, num4);
				agentInput.m_scsRecommended = num4;
			}
			OracleTuningAgent.m_memoryConsumptionToReduce = 0L;
			OracleTuningAgent.m_scanCyclesToSkip = 6;
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000332C8 File Offset: 0x000322C8
		private static void DoWatch()
		{
			if (OracleTuningAgent.m_watchCycles > 0)
			{
				Thread.Sleep(10000);
			}
			long currentVirtualMemorySize = OracleTuningAgent.GetCurrentVirtualMemorySize();
			long num = -1L;
			int availPhysMemory = OpsCom.GetAvailPhysMemory(ref num);
			if (availPhysMemory != 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::DoWatch(): Available Physical Memory Information not found. SCS Reverted \n"
					});
				}
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.REVERT;
				OracleTuningAgent.m_watchCycles = 0;
				return;
			}
			if (currentVirtualMemorySize > OracleTuningAgent.m_veryHighMem || num < OracleTuningAgent.m_minRAMNeeded)
			{
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.REVERT;
				OracleTuningAgent.m_watchCycles = 0;
				return;
			}
			if (--OracleTuningAgent.m_watchCycles > 0)
			{
				OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.WATCH;
				return;
			}
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00033360 File Offset: 0x00032360
		private static void DoRevert()
		{
			try
			{
				if (OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null && OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg != null)
				{
					if (OracleTuningAgent.m_selectedInput.m_scs <= OraTrace.MaxStatementCacheSize)
					{
						OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, OracleTuningAgent.m_selectedInput.m_scs);
						OracleTuningAgent.m_selectedInput.m_scsRecommended = OracleTuningAgent.m_selectedInput.m_scs;
						OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, OracleTuningAgent.m_selectedInput.m_scs);
					}
					else
					{
						int maxStatementCacheSize = OraTrace.MaxStatementCacheSize;
						OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg(OracleTuningAgent.RecommendationType.SCS, maxStatementCacheSize);
						OracleTuningAgent.m_selectedInput.m_scsRecommended = maxStatementCacheSize;
						OracleTuningAgent.SetRecommendedSCS(OracleTuningAgent.m_selectedInputIndex, maxStatementCacheSize);
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						" (ERROR) OracleTuningAgent::DoRevert(): ERROR: " + ex.ToString() + " \n"
					});
				}
			}
			OracleTuningAgent.m_scanCyclesToSkip = 6;
			OracleTuningAgent.m_agentState = OracleTuningAgent.AgentState.SCAN;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00033468 File Offset: 0x00032468
		private static void ResetAllTheTuningFlags()
		{
			for (int i = 0; i < OracleTuningAgent.m_input.Count; i++)
			{
				OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[i] as OracleTuningAgent.AgentInput;
				agentInput.m_scsTuningUptoFESDone = false;
				agentInput.m_scsTuningUptoUniqStmtsDone = false;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000334AC File Offset: 0x000324AC
		private static void ReleaseSamplesFromAllAgentInputs()
		{
			int count = OracleTuningAgent.m_input.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[i] as OracleTuningAgent.AgentInput;
					if (agentInput.m_listOfData.Count > 0)
					{
						lock (agentInput)
						{
							if (agentInput.m_listOfData.Count > 0)
							{
								agentInput.m_listOfData = new ArrayList();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00033538 File Offset: 0x00032538
		private static void SetRecommendedSCS(int agentInputIndex, int scsRecommended)
		{
			OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[agentInputIndex] as OracleTuningAgent.AgentInput;
			agentInput.m_scsRecommended = scsRecommended;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00033560 File Offset: 0x00032560
		private static void SelectPoolToOptimize()
		{
			OracleTuningAgent.AgentInput agentInput = null;
			OracleTuningAgent.m_selectedInput = null;
			OracleTuningAgent.m_selectedInputIndex = -1;
			int count = OracleTuningAgent.m_input.Count;
			int selectedInputIndex = -1;
			if (count != 0)
			{
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					OracleTuningAgent.AgentInput agentInput2 = OracleTuningAgent.m_input[i] as OracleTuningAgent.AgentInput;
					if (agentInput2.m_registered)
					{
						int noOfSubmissions = agentInput2.m_noOfSubmissions;
						if (noOfSubmissions > num && agentInput2.m_listOfData.Count != 0 && !agentInput2.m_scsTuningUptoFESDone)
						{
							num = noOfSubmissions;
							agentInput = agentInput2;
							selectedInputIndex = i;
						}
					}
				}
				bool flag = false;
				if (agentInput == null)
				{
					num = 0;
					for (int j = 0; j < count; j++)
					{
						OracleTuningAgent.AgentInput agentInput3 = OracleTuningAgent.m_input[j] as OracleTuningAgent.AgentInput;
						if (agentInput3.m_registered)
						{
							int noOfSubmissions2 = agentInput3.m_noOfSubmissions;
							if (noOfSubmissions2 > num && agentInput3.m_listOfData.Count != 0)
							{
								flag = true;
								if (!agentInput3.m_scsTuningUptoUniqStmtsDone)
								{
									num = noOfSubmissions2;
									agentInput = agentInput3;
									selectedInputIndex = j;
								}
							}
						}
					}
				}
				if (flag && agentInput == null)
				{
					OracleTuningAgent.ResetAllTheTuningFlags();
					return;
				}
			}
			if (agentInput != null)
			{
				OracleTuningAgent.m_selectedInput = new OracleTuningAgent.AgentInput();
				OracleTuningAgent.m_selectedInput.m_agentKey = agentInput.m_agentKey;
				OracleTuningAgent.m_selectedInput.m_poolConnectionString = agentInput.m_poolConnectionString;
				OracleTuningAgent.m_selectedInput.m_poolId = agentInput.m_poolId;
				OracleTuningAgent.m_selectedInput.m_UpdateRecommendationsDeleg = agentInput.m_UpdateRecommendationsDeleg;
				OracleTuningAgent.m_selectedInput.m_incrementStmtSamplesDeleg = agentInput.m_incrementStmtSamplesDeleg;
				OracleTuningAgent.m_selectedInput.m_registered = agentInput.m_registered;
				OracleTuningAgent.m_selectedInput.m_scs = agentInput.m_scs;
				OracleTuningAgent.m_selectedInput.m_noOfConnections = agentInput.m_noOfConnections;
				OracleTuningAgent.m_selectedInput.m_scsTuningUptoFESDone = agentInput.m_scsTuningUptoFESDone;
				OracleTuningAgent.m_selectedInput.m_scsTuningUptoUniqStmtsDone = agentInput.m_scsTuningUptoUniqStmtsDone;
				lock (agentInput)
				{
					OracleTuningAgent.m_selectedInput.m_listOfData = agentInput.m_listOfData;
					agentInput.m_listOfData = new ArrayList();
					agentInput.m_noOfSubmissions = 0;
				}
				agentInput.m_scsResetDone = false;
				OracleTuningAgent.m_selectedInputIndex = selectedInputIndex;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00033768 File Offset: 0x00032768
		private static ArrayList GetPoolIDsToReduce()
		{
			int count = OracleTuningAgent.m_input.Count;
			ArrayList arrayList = new ArrayList();
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[i] as OracleTuningAgent.AgentInput;
					if (agentInput.m_registered && agentInput.m_scsRecommended > 30)
					{
						arrayList.Add(i);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000337C8 File Offset: 0x000327C8
		internal static void AddData(int poolId, int numberOfConnections, int scs, Hashtable statementData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTuningAgent::AddData()\n"
				});
			}
			OracleTuningAgent.AgentInput agentInput = OracleTuningAgent.m_input[poolId] as OracleTuningAgent.AgentInput;
			if (agentInput == null)
			{
				return;
			}
			lock (agentInput)
			{
				if (agentInput.m_listOfData.Count >= 10)
				{
					agentInput.m_listOfData.RemoveAt(0);
				}
				agentInput.m_listOfData.Add(statementData);
				agentInput.m_scs = scs;
				agentInput.m_noOfConnections = numberOfConnections;
				agentInput.m_noOfSubmissions++;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleTuningAgent::AddData()\n"
				});
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00033894 File Offset: 0x00032894
		private static Hashtable CollateData()
		{
			ArrayList listOfData = OracleTuningAgent.m_selectedInput.m_listOfData;
			Hashtable hashtable = new Hashtable();
			foreach (object obj in listOfData)
			{
				Hashtable hashtable2 = (Hashtable)obj;
				foreach (object obj2 in hashtable2)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					StatementDetails statementDetails = dictionaryEntry.Value as StatementDetails;
					StatementDetails statementDetails2 = hashtable[dictionaryEntry.Key] as StatementDetails;
					if (statementDetails2 == null)
					{
						hashtable[dictionaryEntry.Key] = statementDetails;
					}
					else if (statementDetails.m_executionsIfNotSelect != 0)
					{
						statementDetails2.m_executionsIfNotSelect += statementDetails.m_executionsIfNotSelect;
					}
				}
			}
			return hashtable;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00033998 File Offset: 0x00032998
		private static long GetCurrentVirtualMemorySize()
		{
			Process currentProcess = Process.GetCurrentProcess();
			long num = currentProcess.VirtualMemorySize64;
			currentProcess.Dispose();
			if (num < 0L && num >= -2147483648L)
			{
				num = (long)((ulong)((uint)num));
			}
			return num;
		}

		// Token: 0x040002C5 RID: 709
		internal const string ORA01000 = "ORA-01000";

		// Token: 0x040002C6 RID: 710
		internal const float StatementCacheDecrementForOra01000 = 0.9f;

		// Token: 0x040002C7 RID: 711
		internal const float IgnoreStatementCacheDecrementPercentage = 0.9f;

		// Token: 0x040002C8 RID: 712
		internal const float MinPercentOfRAMForTuning = 0.3f;

		// Token: 0x040002C9 RID: 713
		internal const float MinPercentOfRAMNeeded = 0.2f;

		// Token: 0x040002CA RID: 714
		internal const int m_privateBytesPerStmt = 51200;

		// Token: 0x040002CB RID: 715
		internal const int MaxSubmissionsToBeProcessed = 10;

		// Token: 0x040002CC RID: 716
		internal const int DefaultStmtSamplesLimit = 1000;

		// Token: 0x040002CD RID: 717
		internal const int StmtSampleIncrement = 100;

		// Token: 0x040002CE RID: 718
		private const int ScanInterval = 10000;

		// Token: 0x040002CF RID: 719
		private const int SuspendInterval = 10000;

		// Token: 0x040002D0 RID: 720
		private const int MinExecutionsNeeded = 10;

		// Token: 0x040002D1 RID: 721
		private const int NoOfInternallyExecutedStmts = 5;

		// Token: 0x040002D2 RID: 722
		private const int MinSCSIncrement = 5;

		// Token: 0x040002D3 RID: 723
		private const int MinBufferCnt = 2;

		// Token: 0x040002D4 RID: 724
		private const double SCSBufferPercentage = 0.2;

		// Token: 0x040002D5 RID: 725
		private const int WaitInterval = 5000;

		// Token: 0x040002D6 RID: 726
		private const int WatchInterval = 10000;

		// Token: 0x040002D7 RID: 727
		private const float HighMemPercentage = 0.7f;

		// Token: 0x040002D8 RID: 728
		private const float VeryHighMemPercentage = 0.8f;

		// Token: 0x040002D9 RID: 729
		internal static readonly long m_minRAMReqdForTuning = -1L;

		// Token: 0x040002DA RID: 730
		internal static readonly long m_minRAMNeeded = -1L;

		// Token: 0x040002DB RID: 731
		private static readonly bool m_isUsableMemInfoAvail = false;

		// Token: 0x040002DC RID: 732
		private static readonly long m_highMem = -1L;

		// Token: 0x040002DD RID: 733
		private static readonly long m_installedRAM = -1L;

		// Token: 0x040002DE RID: 734
		private static readonly long m_veryHighMem = -1L;

		// Token: 0x040002DF RID: 735
		private static long m_memoryConsumptionToReduce = 0L;

		// Token: 0x040002E0 RID: 736
		internal static bool bHighMemoryAlertFlag = false;

		// Token: 0x040002E1 RID: 737
		private static OracleTuningAgent.AgentState m_agentState = OracleTuningAgent.AgentState.INIT;

		// Token: 0x040002E2 RID: 738
		private static Thread m_tuningThread = null;

		// Token: 0x040002E3 RID: 739
		private static bool m_allPoolsHaveUnregistered = false;

		// Token: 0x040002E4 RID: 740
		private static int m_numberOfRegistrations = 0;

		// Token: 0x040002E5 RID: 741
		private static OracleTuningAgent.AgentInput m_selectedInput = null;

		// Token: 0x040002E6 RID: 742
		private static int m_selectedInputIndex = -1;

		// Token: 0x040002E7 RID: 743
		private static int m_watchCycles = 1;

		// Token: 0x040002E8 RID: 744
		private static int m_scanCyclesToSkip = 1;

		// Token: 0x040002E9 RID: 745
		private static object m_registrationLock = new object();

		// Token: 0x040002EA RID: 746
		private static ArrayList m_input = new ArrayList();

		// Token: 0x0200005D RID: 93
		private enum AgentState
		{
			// Token: 0x040002EC RID: 748
			INIT,
			// Token: 0x040002ED RID: 749
			WAIT,
			// Token: 0x040002EE RID: 750
			SCAN,
			// Token: 0x040002EF RID: 751
			REDUCE,
			// Token: 0x040002F0 RID: 752
			OPTIMIZE,
			// Token: 0x040002F1 RID: 753
			WATCH,
			// Token: 0x040002F2 RID: 754
			REVERT
		}

		// Token: 0x0200005E RID: 94
		internal enum RecommendationType
		{
			// Token: 0x040002F4 RID: 756
			SCS
		}

		// Token: 0x0200005F RID: 95
		private class AgentInput
		{
			// Token: 0x040002F5 RID: 757
			internal int m_agentKey;

			// Token: 0x040002F6 RID: 758
			internal int m_poolId;

			// Token: 0x040002F7 RID: 759
			internal string m_poolConnectionString;

			// Token: 0x040002F8 RID: 760
			internal OracleTuningAgent.UpdateRecommendations m_UpdateRecommendationsDeleg;

			// Token: 0x040002F9 RID: 761
			internal OracleTuningAgent.IncrementStmtSamplesLimit m_incrementStmtSamplesDeleg;

			// Token: 0x040002FA RID: 762
			internal Hashtable m_collatedData;

			// Token: 0x040002FB RID: 763
			internal ArrayList m_listOfData = new ArrayList();

			// Token: 0x040002FC RID: 764
			internal int m_scs;

			// Token: 0x040002FD RID: 765
			internal int m_scsRecommended = 30;

			// Token: 0x040002FE RID: 766
			internal int m_noOfConnections;

			// Token: 0x040002FF RID: 767
			internal bool m_registered = true;

			// Token: 0x04000300 RID: 768
			internal bool m_scsTuningUptoFESDone;

			// Token: 0x04000301 RID: 769
			internal bool m_scsTuningUptoUniqStmtsDone;

			// Token: 0x04000302 RID: 770
			internal bool m_scsResetDone;

			// Token: 0x04000303 RID: 771
			internal int m_noOfSubmissions;
		}

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x06000480 RID: 1152
		internal delegate void UpdateRecommendations(OracleTuningAgent.RecommendationType recommendationType, object recommendation);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000484 RID: 1156
		internal delegate void IncrementStmtSamplesLimit();
	}
}
