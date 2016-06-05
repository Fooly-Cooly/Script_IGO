function IGO_Clean(%cl, %msg)
{
	%len = strlen(%msg);
	%msg = %len > $Pref::Server::MaxChatLen ? getSubStr(%msg, 0, $Pref::Server::MaxChatLen) : %msg;
	%msg = $Pref::server::eTardFilter && !chatFilter(%cl, %msg, $Pref::Server::ETardList, '\c5This is a civilized game.  Please use full words.') ? 0 : %msg;
	return %msg;
}

function IGO_MessageAdmin(%lvl, %msg)
{
	%i = 0;
	while((%cl = ClientGroup.getObject(%i)) && (%i++))
		if((%cl.isSuperAdmin + %cl.isAdmin) == %lvl)
			messageClient(%cl, '', %msg);
}

function IGOCmd_Admin(%cl, %msg)
{
	%type = %cl.isLan() + %cl.isSuperAdmin + %cl.isAdmin;
	switch(%type)
	{
		case 1: %admin = "Admin";
		case 2: %admin = "Super Admin";
		case 3: %admin = "Host";
		default:return;
	}
	if((%msg = IGO_Clean(%cl, %msg)) !$= "")
	{
		%msg = "[\c3" @ %admin @ "\c0] \c7" @ %cl.ClanPrefix @ "\c3" @ %cl.name @ "\c7" @ %cl.ClanSuffix @ "\c6: \c0" @ %msg;
		IGO_MessageAdmin(1, %msg);
	}
}

function IGOCmd_Announce(%cl, %msg)
{
	if(%cl.isAdmin && (%msg = IGO_Clean(%cl, %msg)) !$= "")
		bottomPrintAll("\c3Announcement: " @ "\c0" @ %msg, 25, 3);
}

function IGOCmd_R(%cl, %msg)
{
	IGOCmd_Whis(%cl, %cl.reply SPC %msg);
}

function IGOCmd_Whis(%cl, %msg)
{
	%vic = firstWord(%msg);
	%msg = restWords(%msg);
	%vic = findClientByName(%vic);
	if(!isObject(%vic))
		messageClient(%cl, '', 'Player not found!');
	else if((%msg = IGO_Clean(%cl, %msg)) !$= "")
	{
		messageClient(%vic, '', '[\c3Whis\c0] \c3%1\c6: %2', %cl.name, %msg);
		messageClient(%cl, '', '[\c3Whis to %1\c0]\c6: %2', %vic.name, %msg);
		%vic.reply = %cl.name;
		%cl.reply = %vic.name;
	}
}

function serverCmdClearChat(%cl)
{
	if(!%cl.isAdmin) return;
	if($Sim::Time > $IGO::LastChatClear)
	{	
		for(%i=0; %i < 1000; %i++) messageAll('', ' ');
		$IGO::LastChatClear = $Sim::Time + 4;	
	}
	else commandToClient(%cl, 'centerprint', '\c3Please Wait...', 3);
}