#pragma strict

 var target: Transform;
 var speed: float;

function Start () {
	  
}

function Update () {
  /*  var vectorToTarget: Vector3 = target.position - transform.position;
 var angle: float = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
 var q: Quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
 transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);*/
    transform.LookAt(target, Vector3.forward);
}
