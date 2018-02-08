﻿using gprotocol;
namespace com.game.client
{
	namespace network.facade
	{
		[NetFacadeAttribute(Module.login)]
		public class LoginNetFacade
		{
			[NetCommandAttribute(Command.login_heart)]
			private void OnReceive_Login_Heart(int code, login_heart_s2c vo)
			{
				//UnityEngine.Debug.Log ("[" + System.DateTime.Now +  "]" + "[OnReceive_Login_Heart]:心跳");
			}

			[NetCommandAttribute(Command.login_login)]
			private void OnReceive_Login_Login(int code, login_login_s2c vo)
			{
				UnityEngine.Debug.Log ("[" + System.DateTime.Now +  "]" + "[OnReceive_Login_Login]:code:" + vo.code);

				if (vo.code == 0) {
					LoginModule.GetInstance ().OnReceive_Login (vo);
				} else {
					if (vo.code == 12)//没有角色
					{
						LoginModule.GetInstance ().ShowPanel_CreateRole ();
					}
				}
			}

			[NetCommandAttribute(Command.login_select_role)]
			private void OnReceive_Login_SelectRole(int code, login_select_role_s2c vo){
				UnityEngine.Debug.Log ("[" + System.DateTime.Now +  "]" + "[OnReceive_Login_SelectRole]:roleID----------------");
				LoginModule.GetInstance ().OnReceive_SelectRole (vo);
			}

			[NetCommandAttribute(Command.login_auth_key)]
			private void OnReceive_Auth_Key(int code, login_auth_key_s2c vo)
			{
				UnityEngine.Debug.Log ("[" + System.DateTime.Now +  "]" + "[OnReceive_Auth_Key]:加密参数, 开启心跳，频率：" + NetWorkConst.Heart_Time + " 秒一次");
				NetWorkManager.Instace.HeartSwitch (true);
			}

			[NetCommandAttribute(Command.login_wait_info)]
			private void OnReceive_Wait_Info(int code, login_wait_info_s2c vo)
			{
				//UnityEngine.Debug.Log ("[" + System.DateTime.Now +  "]" + "[OnReceive_Wait_Info]:排队信息");
			}
		}
	}
}