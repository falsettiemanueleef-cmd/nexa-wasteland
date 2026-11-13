extends Control

# NEXA: WASTELAND — Prototype 0.1
# Placeholder combat slice built with native Godot drawing primitives.
# Art, animation and final balance are intentionally not represented yet.

const BG := Color("#151a1f")
const GROUND := Color("#343a3e")
const ACCENT := Color("#d69a4b")
const PLAYER_COLOR := Color("#6ca6b8")
const ENEMY_COLOR := Color("#a65d4b")
const BOSS_COLOR := Color("#8d4c68")
const UI_PANEL := Color("#242c33")

var player_x := 180.0
var player_hp := 100.0
var enemy_hp := 55.0
var enemy_max_hp := 55.0
var enemy_x := 650.0
var enemy_is_boss := false
var enemy_defeated := false

var move_left := false
var move_right := false
var auto_attack_timer := 0.0
var enemy_attack_timer := 0.0
var combo_timer := 0.0
var combo_step := 0
var special_cooldown := 0.0
var message := "Avvicinati al nemico."
var elapsed := 0.0

var status_label: Label
var enemy_label: Label
var message_label: Label
var hp_label: Label
var left_button: Button
var right_button: Button


func _ready() -> void:
	_build_interface()
	queue_redraw()


func _process(delta: float) -> void:
	elapsed += delta
	var width := get_viewport_rect().size.x

	if move_left:
		player_x -= 260.0 * delta
	if move_right:
		player_x += 260.0 * delta
	player_x = clampf(player_x, 48.0, width - 48.0)

	auto_attack_timer = maxf(0.0, auto_attack_timer - delta)
	enemy_attack_timer = maxf(0.0, enemy_attack_timer - delta)
	combo_timer = maxf(0.0, combo_timer - delta)
	special_cooldown = maxf(0.0, special_cooldown - delta)

	if not enemy_defeated and absf(enemy_x - player_x) <= 150.0 and auto_attack_timer <= 0.0:
		_apply_damage(7.0, "Attacco automatico!")
		auto_attack_timer = 1.15

	if enemy_defeated and not enemy_is_boss:
		_spawn_boss()

	if not enemy_defeated and absf(enemy_x - player_x) <= 105.0 and enemy_attack_timer <= 0.0:
		var incoming_damage := 14.0 if enemy_is_boss else 6.0
		player_hp = maxf(0.0, player_hp - incoming_damage)
		message = "Hai subito " + str(int(incoming_damage)) + " danni!"
		enemy_attack_timer = 1.8 if enemy_is_boss else 2.4
	if player_hp <= 0.0:
		message = "SEI A TERRA: ricarica la scena per riprovare."

	if enemy_defeated and enemy_is_boss:
		message = "Boss sconfitto! Prototipo completato."

	if status_label:
		status_label.text = "NEXA-01  |  PROTOTYPE 0.1"
	if enemy_label:
		if enemy_defeated and enemy_is_boss:
			enemy_label.text = "AREA SICURA"
		else:
			enemy_label.text = ("BOSS" if enemy_is_boss else "RAIDER") + "  HP: " + str(ceili(enemy_hp)) + "/" + str(ceili(enemy_max_hp))
	if message_label:
		message_label.text = message
	if hp_label:
		hp_label.text = "VITA: " + str(ceili(player_hp)) + "/100   |   ABILITA: " + ("PRONTA" if special_cooldown <= 0.0 else str(snappedf(special_cooldown, 0.1)) + "s")

	queue_redraw()


func _build_interface() -> void:
	status_label = Label.new()
	status_label.position = Vector2(20, 14)
	status_label.add_theme_font_size_override("font_size", 20)
	status_label.add_theme_color_override("font_color", ACCENT)
	add_child(status_label)

	enemy_label = Label.new()
	enemy_label.anchor_left = 1.0
	enemy_label.anchor_right = 1.0
	enemy_label.offset_left = -250
	enemy_label.offset_right = -20
	enemy_label.offset_top = 18
	enemy_label.horizontal_alignment = HORIZONTAL_ALIGNMENT_RIGHT
	enemy_label.add_theme_color_override("font_color", Color("#f0d9c1"))
	add_child(enemy_label)

	hp_label = Label.new()
	hp_label.position = Vector2(20, 48)
	hp_label.add_theme_color_override("font_color", Color("#d7e0e5"))
	add_child(hp_label)

	message_label = Label.new()
	message_label.anchor_left = 0.5
	message_label.anchor_right = 0.5
	message_label.offset_left = -220
	message_label.offset_right = 220
	message_label.offset_top = 86
	message_label.horizontal_alignment = HORIZONTAL_ALIGNMENT_CENTER
	message_label.add_theme_color_override("font_color", ACCENT)
	add_child(message_label)

	left_button = _make_button("◀", 0.02, 0.80, 0.13, 0.96)
	left_button.button_down.connect(func(): move_left = true)
	left_button.button_up.connect(func(): move_left = false)

	right_button = _make_button("▶", 0.16, 0.80, 0.27, 0.96)
	right_button.button_down.connect(func(): move_right = true)
	right_button.button_up.connect(func(): move_right = false)

	var combo_button := _make_button("COMBO", 0.69, 0.80, 0.83, 0.96)
	combo_button.pressed.connect(_manual_combo)

	var special_button := _make_button("SPECIAL", 0.84, 0.80, 0.98, 0.96)
	special_button.pressed.connect(_use_special)


func _make_button(caption: String, left: float, top: float, right: float, bottom: float) -> Button:
	var button := Button.new()
	button.text = caption
	button.anchor_left = left
	button.anchor_top = top
	button.anchor_right = right
	button.anchor_bottom = bottom
	button.offset_left = 2
	button.offset_top = 2
	button.offset_right = -2
	button.offset_bottom = -2
	button.add_theme_font_size_override("font_size", 18)
	button.add_theme_color_override("font_color", Color("#f5eee4"))
	var style := StyleBoxFlat.new()
	style.bg_color = UI_PANEL
	style.border_color = ACCENT
	style.set_border_width_all(2)
	style.set_corner_radius_all(10)
	button.add_theme_stylebox_override("normal", style)
	add_child(button)
	return button


func _manual_combo() -> void:
	if enemy_defeated or absf(enemy_x - player_x) > 190.0:
		message = "Troppo lontano per la combo."
		return
	combo_step = (combo_step % 3) + 1 if combo_timer > 0.0 else 1
	combo_timer = 1.0
	var damage := 10.0 + float(combo_step) * 3.0
	_apply_damage(damage, "Combo " + str(combo_step) + "!  -" + str(int(damage)) + " HP")


func _use_special() -> void:
	if special_cooldown > 0.0:
		message = "Abilita in ricarica."
		return
	if enemy_defeated or absf(enemy_x - player_x) > 260.0:
		message = "Nessun bersaglio a portata."
		return
	_apply_damage(24.0, "IMPATTO NEXA!  -24 HP")
	special_cooldown = 8.0


func _apply_damage(amount: float, feedback: String) -> void:
	if enemy_defeated:
		return
	enemy_hp = maxf(0.0, enemy_hp - amount)
	message = feedback
	if enemy_hp <= 0.0:
		enemy_defeated = true
		message = "Nemico eliminato! Preparati al boss."


func _spawn_boss() -> void:
	enemy_is_boss = true
	enemy_defeated = false
	enemy_max_hp = 180.0
	enemy_hp = enemy_max_hp
	enemy_x = maxf(get_viewport_rect().size.x - 190.0, player_x + 180.0)
	message = "BOSS: IL CARNEFICE. Preparati!"


func _draw() -> void:
	var viewport_size := get_viewport_rect().size
	var ground_y := viewport_size.y - 112.0
	draw_rect(Rect2(Vector2.ZERO, viewport_size), BG)
	# Distant ruined skyline
	for i in range(8):
		var bx := float(i) * viewport_size.x / 7.0
		var bh := 70.0 + float((i * 37) % 95)
		draw_rect(Rect2(Vector2(bx, ground_y - bh - 22.0), Vector2(52, bh)), Color("#20272c"))
		draw_rect(Rect2(Vector2(bx + 10, ground_y - bh - 12.0), Vector2(7, 5)), Color("#7d6549"))
	draw_rect(Rect2(Vector2(0, ground_y), Vector2(viewport_size.x, viewport_size.y - ground_y)), GROUND)
	draw_line(Vector2(0, ground_y), Vector2(size.x, ground_y), ACCENT, 3.0)

	# Player placeholder silhouette
	draw_rect(Rect2(Vector2(player_x - 17, ground_y - 68), Vector2(34, 48)), PLAYER_COLOR)
	draw_circle(Vector2(player_x, ground_y - 80), 14, Color("#d5b99a"))
	draw_line(Vector2(player_x + 12, ground_y - 46), Vector2(player_x + 38, ground_y - 30), ACCENT, 6.0)
	draw_rect(Rect2(Vector2(player_x - 22, ground_y - 20), Vector2(16, 20)), Color("#424f59"))
	draw_rect(Rect2(Vector2(player_x + 6, ground_y - 20), Vector2(16, 20)), Color("#424f59"))

	# Enemy / boss placeholder
	if not enemy_defeated:
		var body_color := BOSS_COLOR if enemy_is_boss else ENEMY_COLOR
		var body_size := Vector2(76, 82) if enemy_is_boss else Vector2(44, 54)
		draw_rect(Rect2(Vector2(enemy_x - body_size.x / 2.0, ground_y - body_size.y), body_size), body_color)
		draw_circle(Vector2(enemy_x, ground_y - body_size.y - 10), 17 if enemy_is_boss else 11, Color("#c7a58d"))
		draw_line(Vector2(enemy_x - 14, ground_y - 35), Vector2(enemy_x - 42, ground_y - 12), Color("#c5c1b8"), 5.0)
		# Enemy health bar
		var bar_width := 100.0 if enemy_is_boss else 70.0
		draw_rect(Rect2(Vector2(enemy_x - bar_width / 2.0, ground_y - body_size.y - 38), Vector2(bar_width, 7)), Color("#4b3030"))
		draw_rect(Rect2(Vector2(enemy_x - bar_width / 2.0, ground_y - body_size.y - 38), Vector2(bar_width * enemy_hp / enemy_max_hp, 7)), Color("#c65b50"))

	# Player health bar
	draw_rect(Rect2(Vector2(20, 78), Vector2(150, 9)), Color("#493638"))
	draw_rect(Rect2(Vector2(20, 78), Vector2(150 * player_hp / 100.0, 9)), Color("#65a879"))
