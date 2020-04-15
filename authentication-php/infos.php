<?php
if (isset($_POST['username'])) {
	
	if(!@include("settings.php")){
		die("Code: #");
	}
	
	$username = mysqli_real_escape_string($mysqli,$_POST['username']);
	
	$sql = "SELECT * FROM ".$dbtable." WHERE username='".$username."'";
	$result = $mysqli->query($sql);
	
	if (!$result) {
		die("Code: #");
	}

	$userRow = $result->fetch_assoc();

	if (!$userRow)  {
		die("Code: #");
	}
	
	echo($userRow['subleft']);
	echo "\r\n";
	echo($userRow['subtype']);
	
	$mysqli->Close();
}
?>
