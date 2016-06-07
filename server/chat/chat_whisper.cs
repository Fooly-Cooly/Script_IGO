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