<?php
if (isset($_POST['username']) && isset($_POST['password']))
{
	if(!@include("settings.php")){
		die("Code: #");
	}
	
	$username	    = mysqli_real_escape_string($mysqli,$_POST['username']);
	$password	    = mysqli_real_escape_string($mysqli,$_POST['password']);
	$active         = "true";
	$today          = date("Y-m-d H:i:s");
	
	$sql = "SELECT * FROM ".$dbtable." WHERE username='".$username."'";
	$result = $mysqli->query($sql);

	if (!$result) {
		die("Code: #");
	}

	$userRow = $result->fetch_assoc();

	if (!$userRow)  {
		die("Code: #");
	}
	
	if ($userRow['try'] == "4") {
		die ("Code: #");
	}
	
	if ($password != $userRow['password']) {
		if ($userRow['try'] == "0") {
			$cmd = "UPDATE ".$dbtable." SET try='1' WHERE username='".$username."'";
			$mysqli->query($cmd);
			die("Code: #");
		}
		if ($userRow['try'] == "1") {
			$cmd = "UPDATE ".$dbtable." SET try='2' WHERE username='".$username."'";
			$mysqli->query($cmd);
			die("Code: #");
		}
		if ($userRow['try'] == "2") {
			$cmd = "UPDATE ".$dbtable." SET try='3' WHERE username='".$username."'";
			$mysqli->query($cmd);
			die("Code: #");
		}
		if ($userRow['try'] == "3") {
			$cmd = "UPDATE ".$dbtable." SET try='4' WHERE username='".$username."'";
			$mysqli->query($cmd);
			die("Code: #");
		}
	}

	$cmd = "UPDATE ".$dbtable." SET try='0' WHERE username='".$username."'";
	$mysqli->query($cmd);
	
	if ($active != $userRow['active']) {
		die("Code: #");
	} 
	
	$subleft = $userRow['subleft'];
	
	if ($subleft < $today)
	{
		$cmd = "UPDATE ".$dbtable." SET active='false' WHERE username='".$username."'";
		$mysqli->query($cmd);
		
		die("Code: #");
	}
			
	die("Code: # ");
	
	$mysqli->Close();
}
?>
