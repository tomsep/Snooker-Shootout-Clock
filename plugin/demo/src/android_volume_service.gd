extends Node2D

var _plugin_name = "SnookerShootoutClock"
var _android_plugin

func _ready():
	if Engine.has_singleton(_plugin_name):
		_android_plugin = Engine.get_singleton(_plugin_name)
	else:
		printerr("Couldn't find plugin " + _plugin_name)
		
func get_volume() -> int:
	if _android_plugin == null:
		return 0
	return _android_plugin.getVolume()

func get_max_volume() -> int:
	if _android_plugin == null:
		return 10
	return _android_plugin.getMaxVolume()

func set_volume(volume: int):
	if _android_plugin == null:
		return
	return _android_plugin.setVolume(volume)
