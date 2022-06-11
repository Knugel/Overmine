---
title: Creating a Texture Pack
---

:::caution

Currently changing animations is not supported!

:::

Texture packs allow you to overwrite existing Textures without having to unpack and repack the games assets.
<br/>

To create a texture pack simply create a folder in `<your steamapps folder>\common\UnderMine\Texture Packs\` with the name of your pack.

To replace a texture simply place a .png with the name of the target into your packs folder.
#### Example
If I wanted to replace the texture of Vorpal Blade I would simply create a .png called `VorpalBlade.png` in my pack.

:::info

If 2 packs modify the same texture the one that gets loaded **last** wins.

:::


## Extracting existing game textures
To make it easier to know what textures exists and what their names are Overmine includes an exporter.

Get the [latest version of the exporter](https://github.com/Knugel/Overmine/releases) and extract it to `<your steamapps folder>\common\UnderMine\`.

The next time your start the game you will notice a slowdown as it exports all the sprites/textures. (This only happens the first time)

They can then be found under `<your steamapps folder>\common\UnderMine\Overmine\`.
Now you can simply copy an existing texture into your pack and modify it.