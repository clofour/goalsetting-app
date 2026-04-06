import { Button, Group, Input, SegmentedControl, Stack, Textarea, TextInput } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateNorthStar } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateNorthStarBody } from "@/api/endpoints/goal/goal.zod.js";

export default function CreateNorthStarForm() {
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(PostApiGoalCreateNorthStarBody, { sync: true })
    })

    const handleSubmit = (values: typeof form.values) => {
        postApiGoalCreateNorthStar(values);
    };

    return (
        <form onSubmit={form.onSubmit(handleSubmit)}>
            <Stack>
                <TextInput
                    label="Name"
                    description="What is your goal?"
                    placeholder="Be healthy"
                    required
                    key={form.key('name')}
                    {...form.getInputProps('name')}
                />
                <Textarea
                    label="Description"
                    description="What does your goal consist of?"
                    placeholder="Be healthy"
                    required
                    key={form.key('description')}
                    {...form.getInputProps('description')}
                />
                <Textarea
                    label="Justification"
                    description="Why do you want to achieve this goal? How is it linked to your values and your identity?"
                    placeholder="Be healthy"
                    key={form.key('justification')}
                    {...form.getInputProps('justification')}
                />
                <Input.Wrapper>
                    <SegmentedControl
                        data={[{value: 0, label: "High"}, {value: 1, label: "None"}]}
                        fullWidth
                        key={form.key('importance')}
                        {...form.getInputProps('importance')}
                    />
                </Input.Wrapper>

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </Stack>
        </form>
    )
}