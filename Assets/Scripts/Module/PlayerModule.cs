﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModule : Singleton<PlayerModule>
{
	private gprotocol.p_role _roleInfo;

	public uint RoleID
	{
		get{ 
			return _roleInfo.id;
		}
	}

	/** 职业，0-战士 1-法师 2-弓箭手 3-道士 */
	public uint RoleJob{
		get{ 
			return _roleInfo.job;
		}
	}

	public void SetRoleInfo(gprotocol.p_role roleInf){
		_roleInfo = roleInf;
	}
}
