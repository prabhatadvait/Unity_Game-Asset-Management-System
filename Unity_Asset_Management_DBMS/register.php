<?php

$servername = "localhost";
$username = "root";
$password = "prabhat"; 
$dbname = "unityaccess";

// Create connection
$con = new mysqli($servername, $username, $password, $dbname);


if ($con->connect_error) {
    die("Connection failed: " . $con->connect_error);
    exit();
}
echo "Connected successfully";


$username = $_POST["name"];
$password = $_POST["password"];

//check if name exists
$namecheckquery = "SELECT username FROM players WHERE username='" .$username ."';";

$namecheck = mysqli_query($con,$namecheckquery) or die("2: namecheckquery failed"); //error code 2 = namecheckquery failed

if (mysqli_num_rows($namecheck) > 0)
{
    echo "3: name already exists"; //error 3    
    exit();
}

//add user to table
$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
$hash = crypt($password,$salt);
$insertuserquery = "INSERT INTO players (username,hash,salt) VALUES('" . $username . "', '" . $hash . "', '" . $salt . "');";
mysqli_query($con, $insertuserquery) or die("4: Insert player query failed");  //error 4

echo("0");

?>