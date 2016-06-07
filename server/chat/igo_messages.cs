function IGOCmd_Announce(%cl, %msg)
{
	if(%cl.isAdmin && (%msg = IGO_Clean(%cl, %msg)) !$= "")
		bottomPrintAll("\c3Announcement: " @ "\c0" @ %msg, 25, 3);
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