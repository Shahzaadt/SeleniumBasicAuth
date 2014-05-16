/*
 * Created by SharpDevelop.
 * User: Alan
 * Date: 5/8/2014
 * Time: 12:34 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace SeleniumBasicAuth
{
	/// <summary>
	/// Description of BasicAuthObject.
	/// </summary>
	public class BasicAuthObject
	{
		public String username {get; set;}
		public String password {get; set;}
		
		public BasicAuthObject(String username, String password)
		{
			this.username = username;
			this.password = password;
		}
	}
}
