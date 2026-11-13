extends Control

const BACKGROUND := Color("#171b20")
const PANEL := Color("#252d34")
const ACCENT := Color("#c58b45")
const TEXT_COLOR := Color("#eee7da")


func _ready() -> void:
    _build_screen()


func _build_screen() -> void:
    var background := ColorRect.new()
    background.color = BACKGROUND
    background.set_anchors_and_offsets_preset(Control.PRESET_FULL_RECT)
    add_child(background)

    var center := CenterContainer.new()
    center.set_anchors_and_offsets_preset(Control.PRESET_FULL_RECT)
    add_child(center)

    var panel := PanelContainer.new()
    panel.custom_minimum_size = Vector2(300, 220)
    center.add_child(panel)

    var style := StyleBoxFlat.new()
    style.bg_color = PANEL
    style.set_corner_radius_all(12)
    style.content_margin_left = 24
    style.content_margin_right = 24
    style.content_margin_top = 24
    style.content_margin_bottom = 24
    panel.add_theme_stylebox_override("panel", style)

    var column := VBoxContainer.new()
    column.add_theme_constant_override("separation", 14)
    panel.add_child(column)

    var title := Label.new()
    title.text = "NEXA: WASTELAND"
    title.horizontal_alignment = HORIZONTAL_ALIGNMENT_CENTER
    title.add_theme_color_override("font_color", ACCENT)
    title.add_theme_font_size_override("font_size", 24)
    column.add_child(title)

    var subtitle := Label.new()
    subtitle.text = "THE COLLAPSE IS OVER.\nSURVIVAL IS JUST BEGINNING."
    subtitle.horizontal_alignment = HORIZONTAL_ALIGNMENT_CENTER
    subtitle.add_theme_color_override("font_color", TEXT_COLOR)
    column.add_child(subtitle)

    var status := Label.new()
    status.text = "NEXA-01 // SYSTEM ONLINE\nPrototype v0.1"
    status.horizontal_alignment = HORIZONTAL_ALIGNMENT_CENTER
    status.add_theme_color_override("font_color", Color("#aab4bd"))
    column.add_child(status)

    var button := Button.new()
    button.text = "CONTINUE"
    button.custom_minimum_size = Vector2(0, 48)
    button.pressed.connect(_on_continue_pressed)
    column.add_child(button)


func _on_continue_pressed() -> void:
    print("NEXA-01: first prototype screen is running.")
