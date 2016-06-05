function serverCmdClearSpam(%cl,%opt)
{
	if(%opt < 1 || !%cl.isAdmin) return;
	for(%i=0; %i < mainBrickGroup.getCount(); %i++)
	{
		%grp = mainBrickGroup.getObject(%i);
		for(%j=0; %j < %grp.getCount(); %j++)
		{
			%obj = %grp.getObject(%j);
			switch(%opt)
			{
				case 1: if(!isObject(%obj.getUpBrick(0)) && !isObject(%obj.getDownBrick(0)))
							if(!isObject(%obj.Vehicle.getControllingObject()))
								%obj.KillBrick();
				case 2: if(!isObject(%obj.Vehicle.getControllingObject()))
							%obj.Vehicle.delete();
				case 3: if(isObject(%obj.vehicle))
							%obj.Vehicle.delete();
			}
		}
	}
	switch(%opt)
	{
		case 1:	messageAll('',%cl.name SPC "\c3cleared all lone spam bricks.");
		case 2:	messageAll('',%cl.name SPC "\c3cleared all empty vehicles.");
		case 3:	messageAll('',%cl.name SPC "\c3cleared all vehicles.");
	}
}