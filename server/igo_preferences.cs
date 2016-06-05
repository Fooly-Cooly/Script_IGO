function IGO_SavePrefs()
{
	export("$Pref::Server::*","config/server/prefs.cs",False);
	export("$Pref::Net::PacketRateToClient","config/server/prefs.cs",True);
	export("$Pref::Net::PacketRateToServer","config/server/prefs.cs",True);
	export("$Pref::Net::PacketSize","config/server/prefs.cs",True);
	export("$Pref::Net::LagThreshold","config/server/prefs.cs",True);
}

function serverCmdIGO_Update(%cl)
{
	CommandToClient(%cl, 'IGO_Update', "Button", "FloodProtect", $Pref::Server::FloodProtectionEnabled);
	CommandToClient(%cl, 'IGO_Update', "Button", "FallDamage", $Pref::Server::FallingDamage);
	CommandToClient(%cl, 'IGO_Update', "Button", "Etard", $Pref::Server::ETardFilter);
	CommandToClient(%cl, 'IGO_Update', "Text", "txtMaxPlayers", $Pref::Server::MaxPlayers);
}

function serverCmdIGSO(%Client,%Set,%Arg)
{
	if(%Client.isSuperAdmin)
	{
		switch(%Set)
		{
			case 0:	if(%Arg !$= "")
				{
					%Arg = trim(%Arg);
					%Arg = strreplace(%Arg,":red:","\c0");
					%Arg = strreplace(%Arg,":blue:","\c1");
					%Arg = strreplace(%Arg,":green:","\c2");
					%Arg = strreplace(%Arg,":yellow:","\c3");
					%Arg = strreplace(%Arg,":teal:","\c4");
					%Arg = strreplace(%Arg,":pink:","\c5");
					%Arg = strreplace(%Arg,":white:","\c6");
					%Arg = strreplace(%Arg,":grey:","\c7");
					%Arg = strreplace(%Arg,":black:","\c8");
				}
				%Msg = "\c3The welcome message is now:";
				$Pref::Server::WelcomeMessage = %Arg;
			case 1:	if(%Arg $= ""){%Arg = "Blockland Retail Server";}
				%Msg = "\c3The server name is now:";
				$Pref::Server::Name = %Arg;
			case 2:	messageall('','\c3The server is now password protected.');
				messageAdmin(1,'','\c3 The new server pass is: \c0%1',%Arg);
				$Pref::Server::Password = %Arg;
				%Arg = "skip";
			case 3:	messageall('','\c3The super admin password has been changed');
				messageAdmin(2,'','\c3The new super admin password is: \c0%1',%Arg);
				$Pref::Server::SuperAdminPassword = %Arg;
				%Arg = "skip";
			case 5:	%Msg = "\c3The port is now:";
				$Pref::Server::Port = %Arg;
			case 6:	%Msg = "\c3The server\'s brick limit is now:";
				$Pref::Server::BrickLimit = %Arg;
			case 7:	%Msg = "\c3Players\' max vehicles is now:";
				$Pref::Server::Quota::Vehicle = %Arg;
			case 8:	%Msg = "\c3The server\' max vehicles is now:";
				$Pref::Server::MaxPhysVehicles_Total = %Arg;
			case 9:	%Msg = "\c3Players\' max lights/emitters are now:";
				$Pref::Server::Quota::Environment = %Arg;
			case 10:%Msg = "\c3Players\' max items are now:";
				$Pref::Server::Quota::Item = %Arg;
			case 11:%Msg = "\c3Projectile Quota is now:";
				$Pref::Server::Quota::Projectile = %Arg;
			case 12:%Msg = "\c3Schedule Quota is now:";
				$Pref::Server::Quota::Schedules = %Arg;
			case 13:%Msg = "\c3Too Far Distance is now:";
				$Pref::Server::TooFarDistance = %Arg;
		}
	}
	if(%Client.isAdmin)
	{
		switch$(%Set)
		{
			case "4":
				if(%Arg $= ""){messageall('','\c3The server no longer has an admin password');}
				else
				{
					messageall('','\c3The admin password has been changed');
					messageAdmin(1,'','\c3The new admin password is: \c0%1',%Arg);
				}
				$Pref::Server::AdminPassword = %Arg;
				%Arg = "skip";
			case "FallDamage":
				switch($Pref::Server::FallingDamage)
				{
					case 0:	$Pref::Server::FallingDamage = 1;
						messageAll("","\c3Fall Damage is now \c0ON.");
					case 1:	$Pref::Server::FallingDamage = 0;
						messageAll("","\c3Fall Damage is now \c0OFF.");
				}
				CommandToAll('UpdateIGSO',"Button","FallDamage",$Pref::Server::FallingDamage);
				%Arg = "skip";
			case "eTard":
				switch($Pref::Server::ETardFilter)
				{
					case 0:	$Pref::Server::ETardFilter = 1;
						messageAll('',"\c3E-Tard Filter is now \c0ON.");
						messageAll('',%Client.name);
					case 1:	$Pref::Server::ETardFilter = 0;
						messageAll('',"\c3E-Tard Filter is now \c0OFF.");
						messageAll('',%Client.name);
				}
				CommandToAll('UpdateIGSO',"Button","Etard",$Pref::Server::ETardFilter);
				%Arg = "skip";
			case "Flood":
				switch($Pref::Server::FloodProtectionEnabled)
				{
					case 0:	$Pref::Server::FloodProtectionEnabled = 1;
						messageAll('',"\c3Flood Protection is now \c0ON.");
					case 1:	$Pref::Server::FloodProtectionEnabled = 0;
						messageAll('',"\c3Flood Protection is now \c0OFF.");
				}
				CommandToAll('UpdateIGSO',"Button","FloodProtect",$Pref::Server::FloodProtectionEnabled);
				%Arg = "skip";
			case "MaxP":
				cancel($MaxPEvnt);
				if($Pref::Server::MaxPlayers < 64)
				{
					$Pref::Server::MaxPlayers++;
					$MaxPEvnt = schedule(2000,0,"messageall",'','\c3Max Players increased to: \c0%1',$Pref::Server::MaxPlayers);
					CommandToAll('UpdateIGSO',"Text","txtMaxPlayers",$Pref::Server::MaxPlayers);
					%Arg = "skip";
				}
			case "MaxS":
				cancel($MaxPEvnt);
				if($Pref::Server::MaxPlayers > ClientGroup.getCount())
				{
					$Pref::Server::MaxPlayers--;
					$MaxPEvnt = schedule(2000,0,"messageall",'','\c3Max Players decreased to: \c0%1',$Pref::Server::MaxPlayers);
					CommandToAll('UpdateIGSO',"Text","txtMaxPlayers",$Pref::Server::MaxPlayers);
					%Arg = "skip";
				}
		}
		if(%Arg !$= "skip"  && %Set < 14 && %Set > -1)
		{
			if(%Arg $="" && %Set < 13 && %Set > 4)
				%Arg = 0;
			messageall('',%Msg SPC "\c0"@%Arg);
		}
		IGO_SavePrefs();
	}
}