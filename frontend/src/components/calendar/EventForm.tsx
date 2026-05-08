import { Button, Checkbox, Combobox, Group, Input, InputBase, NumberInput, Stack, Tabs, Textarea, TextInput, useCombobox } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";
import { getErrorMessage } from "@/data/error";
import { useState } from "react";

interface EventFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
}

export default function EventForm({ close, setAlert }: EventFormProps) {
    const formSchema = PostApiGoalCreateBearingBody.omit({
        northStarId: true
    })
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(formSchema, { sync: true })
    })
    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values
        }
        const response = await postApiGoalCreateBearing(requestData);

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? getErrorMessage(response.status));
        }
    };

    const unitCombobox = useCombobox({
        onDropdownClose: () => unitCombobox.resetSelectedOption()
    })
    const [unit, setUnit] = useState<string | null>(null);
    const unitOptions = ["day", "week", "month", "year"].map((option) => (
        <Combobox.Option value={option} key={option}>
            {option}
        </Combobox.Option>
    ))

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What should this event be called?"
                        placeholder="1"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />


                    <Group grow justify="flex-between">
                        <NumberInput
                            label="Amount"
                            description="What should this event be called?"
                            placeholder="1"
                            required
                            key={form.key('amount')}
                            {...form.getInputProps('amount')}
                        />
                        <Combobox
                            store={unitCombobox}
                            onOptionSubmit={(value) => {
                                setUnit(value);
                                unitCombobox.closeDropdown();
                            }}
                            key={form.key('unit')}
                            {...form.getInputProps('unit')}
                        >
                            <Combobox.Target>
                                <InputBase
                                    label="Unit"
                                    description="What should this event be called?"
                                    required
                                    component="button"
                                    type="button"
                                    pointer
                                    rightSection={<Combobox.Chevron />}
                                    rightSectionPointerEvents="none"
                                    onClick={() => unitCombobox.toggleDropdown()}
                                >
                                    {unit || <Input.Placeholder>Pick unit</Input.Placeholder>}
                                </InputBase>
                            </Combobox.Target>

                            <Combobox.Dropdown>
                                <Combobox.Options>{unitOptions}</Combobox.Options>
                            </Combobox.Dropdown>
                        </Combobox>
                    </Group>

                    {unit == "week" && (
                        <Checkbox.Group
                            label="Day of the week"
                            description="Which day of the week should this event take place?"
                            required
                            key={form.key('weekday')}
                            {...form.getInputProps('weekday')}
                        >
                            <Group mt="xs">
                                {["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].map((weekday) => (
                                    <Checkbox key={weekday} value={weekday} label={weekday} />
                                ))}
                            </Group>
                        </Checkbox.Group>
                    )}

                    {unit == "month" && (
                        <NumberInput
                            label="Day of the month"
                            description="Which day of the month should this event take place?"
                            required
                            key={form.key('monthday')}
                            {...form.getInputProps('monthday')}
                        />
                    )}

                    {unit == "year" && (
                        <Group grow justify="flex-between">
                            <NumberInput
                                label="Day of the month"
                                description="Which day of the month should this event take place?"
                                required
                                key={form.key('monthday')}
                                {...form.getInputProps('monthday')}
                            />
                            <NumberInput
                                label="Month of the year"
                                description="Which month of the eyar should this event take place?"
                                required
                                key={form.key('yearmonth')}
                                {...form.getInputProps('yearmonth')}
                            />
                        </Group>
                    )}

                    <Group justify="flex-end">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}