package tomsep.snookerclock

import org.godotengine.godot.Godot
import org.godotengine.godot.plugin.GodotPlugin
import org.godotengine.godot.plugin.UsedByGodot
import android.content.Context
import android.media.AudioManager

class GodotAndroidPlugin(godot: Godot): GodotPlugin(godot) {

    override fun getPluginName() = BuildConfig.GODOT_PLUGIN_NAME

    @UsedByGodot
    private fun getVolume(): Int {
        val context = activity!!.applicationContext
        val audioManager = context.getSystemService(Context.AUDIO_SERVICE) as AudioManager
        return audioManager.getStreamVolume(AudioManager.STREAM_MUSIC)
    }

    @UsedByGodot
    private fun setVolume(volume: Int) {
        val context = activity!!.applicationContext
        val audioManager = context.getSystemService(Context.AUDIO_SERVICE) as AudioManager

        // Get the maximum volume level for music stream
        val maxVolume = audioManager.getStreamMaxVolume(AudioManager.STREAM_MUSIC)

        // Ensure the volume value is within the valid range
        val validVolume = volume.coerceIn(0, maxVolume)

        // Set the volume level for music stream
        audioManager.setStreamVolume(AudioManager.STREAM_MUSIC, validVolume, 0)
    }

    @UsedByGodot
    private fun getMaxVolume(): Int {
        val context = activity!!.applicationContext
        val audioManager = context.getSystemService(Context.AUDIO_SERVICE) as AudioManager
        return audioManager.getStreamMaxVolume(AudioManager.STREAM_MUSIC)
    }
}
