echo("\n--------- Initializing IGO Client ---------");

//--Main Scripts----------------
exec("./client/igo.cs");
exec("./client/igo_color.cs");
exec("./client/igo_lists.cs");
exec("./client/igo_chathook.cs");

//--Graphical User Interface----
exec("./client/UI/igo.gui");

//--Chat Hooks-------------------
exec("./client/chat/chat_exec.cs");
exec("./client/chat/chat_scriptdump.cs");
exec("./client/chat/chat_grammar.cs");
exec("./client/chat/chat_link.cs");

//--Settings and Info-----------
if(!isFile("Config/Client/IGO/Colors.cs"))
{
	$IGSOColorName[$IGSOTotalColors] = "Blue";
	$IGSOColor[$IGSOTotalColors] = "53 154 255 255";
	$IGSOTotalColors++;
	$IGSOColorName[$IGSOTotalColors] = "Red";
	$IGSOColor[$IGSOTotalColors] = "255 40 40 255";
	$IGSOTotalColors++;
	$IGSOColorName[$IGSOTotalColors] = "Green";
	$IGSOColor[$IGSOTotalColors] = "40 255 40 255";
	$IGSOTotalColors++;
	$IGSOColorName[$IGSOTotalColors] = "Yellow";
	$IGSOColor[$IGSOTotalColors] = "255 255 40 255";
	$IGSOTotalColors++;
	$IGSOColorName[$IGSOTotalColors] = "Orange";
	$IGSOColor[$IGSOTotalColors] = "255 128 40 255";
	$IGSOTotalColors++;
	$IGSOColorName[$IGSOTotalColors] = "Purple";
	$IGSOColor[$IGSOTotalColors] = "128 40 128 255";
	$IGSOTotalColors++;
}
else exec("Config/Client/IGO/Colors.cs");

if(!isFile("Config/Client/IGO/Prefs.cs"))
{
	$IGSOC::OffColor = "255 40 40 255";
	$IGSOC::OnColor = "53 154 255 255";
}
else exec("Config/Client/IGO/Prefs.cs");

echo("Loading IGSO Color List...\n");
IGO_getColorList();

echo("Setting IGSO Button Colors...\n");
IGO_setButtonColor2();

//Add Pref Options--------------
echo("Loading IGSO Settings List...\n");
IGO_Prefs.Clear();
IGO_Prefs.add("Welcome",0);
IGO_Prefs.add("Server Name",1);
IGO_Prefs.add("Server Pass",2);
IGO_Prefs.add("Super Admin Pass",3);
IGO_Prefs.add("Admin Pass",4);
IGO_Prefs.add("Port",5);
IGO_Prefs.add("Brick Limit",6);
IGO_Prefs.add("Max Phys Vehicles (P)",7);
IGO_Prefs.add("Max Phys Vehicles (T)",8);
IGO_Prefs.add("Max Lights/Emitters (P)",9);
IGO_Prefs.add("Max Items (P)",10);
IGO_Prefs.add("Projectile Quota ",11);
IGO_Prefs.add("Schedule Quota",12);
IGO_Prefs.add("Too Far Distance",13);
IGO_Prefs.setSelected(0);

popClearSpam.Clear();
popClearSpam.add("Do Nothing",0);
popClearSpam.add("Lone Bricks",1);
popClearSpam.add("Clear All Vehicles",2);
popClearSpam.add("Clear Empty Vehicles",3);
popClearSpam.setSelected(0);

function ClearSpamGui::ClearSpam(%this)
{
	commandToServer('ClearSpam',popClearSpam.getSelected());
}